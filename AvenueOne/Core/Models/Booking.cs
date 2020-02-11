using AvenueOne.Core.Models.CustomDataAnnotations;
using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using AvenueOne.Utilities.Tools;
using System;
using System.Collections.Generic;
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
                OnPropertyChanged();
            }
        }

        private DateTime _dateCheckout;

        [AfterDateProperty(nameof(DateCheckin))]
        public DateTime DateCheckout
        {
            get { return _dateCheckout; }
            set { _dateCheckout = value;
                OnPropertyChanged();
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

        public int Occupants
        {
            get { return _occupants; }
            set { _occupants = value;
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
        public Room Room
        {
            get { return _room; }
            set { _room = value;
                if(value != null)
                {
                    this.AmountTotal = Booking.ComputeAmountTotal(this.DateCheckin, this.DateCheckout, value.RoomType.Rate, value.RoomType.RateType);
                    OnPropertyChanged("AmountTotal");
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

        public bool IsBookingConflict(Booking booking)
        {
            if (booking.Room == null || this.Room == null)
                return false; // there is no conflict if there is no room.

            return this.DateCheckin >= booking.DateCheckin && this.DateCheckin <= booking.DateCheckout && this.Room.Id == booking.Room.Id ||
                    booking.DateCheckin >= this.DateCheckout && booking.DateCheckout <= this.DateCheckout && this.Room.Id == booking.Room.Id;
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
                    lengthOfStay =  0;
                    break;
            }

            if (lengthOfStay < 0)
                throw new InvalidOperationException("Length of stay must be greater than 0.");

            return rate * lengthOfStay;
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
