using AvenueOne.Core.Models.CustomDataAnnotations;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using AvenueOne.Utilities.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Booking : BaseObservableModel<Booking>, IBooking
    {
        #region Properties
        private DateTime _dateCheckin;

        [BeforeDateProperty(nameof(DateCheckout))]
        public DateTime DateCheckin
        {
            get { return _dateCheckin; }
            set { _dateCheckin = value;
                //if (value != null) //next time make it a checkin or checkout time vlaue.
                //    _dateCheckin = new DateTime(value.Year, value.Month, value.Day);
                if(value != null && Room != null)
                {
                    this.AmountTotal = Booking.ComputeAmountTotal(this.DateCheckin, this.DateCheckout, Room.RoomType.Rate, Room.RoomType.RateType);
                    OnPropertyChanged(nameof(AmountTotal));
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(DateCheckout));
                OnPropertyChanged(nameof(LengthOfStay));


            }
        }

        private DateTime _dateCheckout;

        [AfterDateProperty(nameof(DateCheckin))]
        public DateTime DateCheckout
        {
            get { return _dateCheckout; }
            set { _dateCheckout = value;
                //if (value != null) //next time make it a checkin or checkout time vlaue.
                //    _dateCheckout = new DateTime(value.Year, value.Month, value.Day);
                if (value != null && Room != null)
                {
                    this.AmountTotal = Booking.ComputeAmountTotal(this.DateCheckin, this.DateCheckout, Room.RoomType.Rate, Room.RoomType.RateType);
                    OnPropertyChanged(nameof(AmountTotal));
                }

                OnPropertyChanged();
                OnPropertyChanged(nameof(DateCheckin));
                OnPropertyChanged(nameof(LengthOfStay));
            }
        }

        private decimal _amountTotal;
        public decimal AmountTotal
        {
            get { return _amountTotal; }
            set { _amountTotal = value;
                OnPropertyChanged();
            }
        }

        private int _occupants;

        [Range(0, int.MaxValue, ErrorMessage ="value > 0")]
        public int Occupants
        {
            get { return _occupants; }
            set {
                _occupants = value;
                if (value < 0)
                    _occupants = 0;
                if(Room != null)
                {
                    if (value > Room.MaxOccupants)
                        _occupants = Room.MaxOccupants;
                }

                OnPropertyChanged();
            }
        }


        private BookingStatus _status;
        public BookingStatus Status
        {
            get { return _status; }
            set { _status = value;
                OnPropertyChanged();
            }
        }

        private Room _room;

        [Required(ErrorMessage ="Room is required")]
        public Room Room
        {
            get { return _room; }
            set { _room = value;
                if(value != null)
                {
                    this.AmountTotal = Booking.ComputeAmountTotal(this.DateCheckin, this.DateCheckout, value.RoomType.Rate, value.RoomType.RateType);
                    OnPropertyChanged(nameof(AmountTotal));
                    if (Occupants > value.MaxOccupants)
                    {
                        Occupants = value.MaxOccupants;
                    }
                }
                OnPropertyChanged();
            }
        }

        public TimeSpan LengthOfStay
        {
            get
            {
                return (this.DateCheckout - this.DateCheckin);
            }
        }

        private Transaction _transaction;

        public Transaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value;
                OnPropertyChanged();
            }
        }



        #endregion

        #region Constructors
        public Booking()
                : base()
            {
                DateTime today = DateTime.Today;
                this.DateCheckin = today;
                this.DateCheckout = today.AddDays(1);
                this.Occupants = 1;
                this.Status = BookingStatus.reserved;
        }

        public Booking(DateTime dateCheckin, DateTime dateCheckout, Room room)
            : base()
        {
            this.DateCheckin = dateCheckin;
            this.DateCheckout = dateCheckout;
            this.Room = room;
            this.Status = BookingStatus.reserved;
            this.Occupants = 1;

            decimal rate = this.Room.RoomType.Rate;
            RateType rateType = this.Room.RoomType.RateType;

            this.AmountTotal = ComputeAmountTotal(dateCheckin, dateCheckout, rate, rateType);
        }
        #endregion

        #region Methods
        public bool IsBookingConflict(Booking booking)
        {
            if (booking == null)
                return false; // no conflict if there is no booking
            if (booking.Room == null || this.Room == null)
                return false; // there is no conflict if there is no room.
            if (booking.Room != this.Room)
                return false; //not the same room so no conflict.

            return IsBookingDateConflict(booking);
        }

        public bool IsBookingConflict(Booking booking, Room room)
        {
            if (booking == null)
                return false; // no conflict if there is no booking
            if (room == null || this.Room == null)
                return false; // there is no conflict if there is no room.
            if (booking.Room != room)
                return false; //not the same room so no conflict.

            return IsBookingDateConflict(booking);
        }

        public bool IsBookingDateConflict(Booking booking)
        {
            if (booking == null)
                return false; // no conflict if there is no booking

            bool isConflict = this.DateCheckin.Date >= booking.DateCheckin.Date && this.DateCheckin.Date <= booking.DateCheckout.Date ||
                this.DateCheckout.Date >= booking.DateCheckin.Date && this.DateCheckout.Date <= booking.DateCheckout.Date ||
                booking.DateCheckin.Date >= this.DateCheckin.Date && booking.DateCheckin.Date <= this.DateCheckout.Date ||
                booking.DateCheckout.Date >= this.DateCheckin.Date && booking.DateCheckout.Date <= this.DateCheckout.Date;

            return isConflict;
        }

        public static decimal ComputeAmountTotal(DateTime dateCheckin, DateTime dateCheckout, decimal rate, RateType rateType)
        {
            decimal lengthOfStay = 0;
            TimeSpan timeSpan = (dateCheckout - dateCheckin);
            switch (rateType)
            {
                case RateType.Seconds:
                    lengthOfStay = (decimal)timeSpan.TotalSeconds;
                    break;
                case RateType.Minutes:
                    lengthOfStay = (decimal)timeSpan.TotalMinutes;
                    break;
                case RateType.Hourly:
                    lengthOfStay = (decimal)timeSpan.TotalHours;
                    break;
                case RateType.Daily:
                    lengthOfStay = (decimal)timeSpan.TotalDays;
                    break;
                case RateType.Monthly:
                    lengthOfStay = (decimal)(timeSpan.TotalDays / 30);
                    break;
                case RateType.Yearly:
                    lengthOfStay = (decimal)(timeSpan.TotalDays / 365);
                    break;

                default:
                    lengthOfStay = 0;
                    break;
            }

            if (lengthOfStay < 0)
                throw new InvalidOperationException("Length of stay must be greater than 0.");

            return rate * lengthOfStay;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (!(obj is Booking))
                return false;

            Booking booking = (Booking)obj;

            bool isSameRoom = false;
            if(booking.Room != null && this.Room != null)
                isSameRoom = this.Room.Equals(booking.Room);

            return this.DateCheckin.Year == booking.DateCheckin.Year &&
                this.DateCheckin.Month == booking.DateCheckin.Month &&
                this.DateCheckin.Day == booking.DateCheckin.Day &&
                this.DateCheckout.Year == booking.DateCheckout.Year &&
                this.DateCheckout.Month == booking.DateCheckout.Month &&
                this.DateCheckout.Day == booking.DateCheckout.Day &&
                this.AmountTotal == booking.AmountTotal &&
                this.Status == booking.Status &&
                isSameRoom;
                
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
