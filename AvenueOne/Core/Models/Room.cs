using AvenueOne.Core.Models.Interfaces;
using AvenueOne.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Room : BaseObservableModel<Room>, IRoom
    {
        #region Properties

            private string _name;
            public string Name
            {
                get { return _name; }
                set { _name = value;
                    OnPropertyChanged();
                }
            }

            private int _floor;
            public int Floor
            {
                get { return _floor; }
                set { _floor = value;
                    OnPropertyChanged();
                }
            }

            private int _maxOccupants;
            public int MaxOccupants
            {
                get { return _maxOccupants; }
                set { _maxOccupants = value;
                    OnPropertyChanged();
                }
            }

            private string _details;
            public string Details
            {
                get { return _details; }
                set
                {
                    _details = value;
                    OnPropertyChanged();
                }
            }

            private RoomType _roomType;
            public RoomType RoomType
            {
                get { return _roomType; }
                set { _roomType = value;
                    OnPropertyChanged();
                }
            }

        private ObservableCollection<Booking> _bookings;
        public ObservableCollection<Booking> Bookings
        {
            get { return _bookings; }
            set { _bookings = value;
                OnPropertyChanged();
            }
        }

        //public IList<Booking> Bookings { get; set; }
        #endregion

        #region Constructors
        public Room()
                : base()
            {
            }

        public Room(string name)
            :this()
        {
            this.Name = name;
        }

        public Room(string name, int maxOccupants)
            :this(name)
        {
            this.MaxOccupants = maxOccupants;
        }

        public Room(string name, int maxOccupants, int floor)
            :this(name, maxOccupants)
        {
            this.Floor = floor;
        }
        #endregion

        #region Methods

        public List<RoomStatus> GetAvailabilityForMonth(int year, int month)
        {
            List<RoomStatus> availabilityForMonth = new List<RoomStatus>();
            int daysInMonth = DateTime.DaysInMonth(year, month);
            DateTime date = new DateTime(year, month, 1);

            //no bookings so all true;
            if (Bookings == null || Bookings.Count <= 0)
            {
                for (int i = 0; i < daysInMonth; i++)
                {
                    availabilityForMonth.Add(RoomStatus.vacant);
                }
            }
            else
            {
                List<Booking> bookings = Bookings.Where(b => b.IsBookingDateConflict(date, date.AddDays(daysInMonth-1))).ToList();
                for (int i = 0; i < daysInMonth; i++)
                {
                    RoomStatus isAvailable = RoomStatus.vacant;
                    if (i != 0)
                        date.AddDays(1);

                    foreach (var item in bookings)
                    {
                        if (item.IsDateBetweenBookingDate(date))
                        {
                            if (item.Status == BookingStatus.checkedin)
                            {
                                isAvailable = RoomStatus.occupied;
                                break;
                            }
                            if (item.Status == BookingStatus.reserved)
                            {
                                isAvailable = RoomStatus.booked;
                                break;
                            }
                        }
                    }
                    availabilityForMonth.Add(isAvailable);
                }
            }

            return availabilityForMonth;
        }

        //public bool IsRoomAvailableOnDate(DateTime date)
        //{
        //    if (Bookings == null)
        //        return true;

        //    if (Bookings.Count <= 0)
        //        return true;

        //    foreach (var item in Bookings)
        //    {
        //        if (item.IsDateBetweenBookingDate(date))
        //            return false;
        //    }

        //    return true;
        //}

        public RoomStatus GetRoomStatus(DateTime date)
        {
            if (Bookings == null)
                return RoomStatus.vacant;

            if (Bookings.Count <= 0)
                return RoomStatus.vacant;

            foreach (var item in Bookings)
            {
                if (item.IsDateBetweenBookingDate(date))
                {
                    if(item.Status == BookingStatus.checkedin)
                        return RoomStatus.occupied;
                    if (item.Status == BookingStatus.reserved)
                        return RoomStatus.booked;
                }
            }

            return RoomStatus.vacant;
        }

        #endregion

        #region Overrides
        public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;

                if (!(obj is Room))
                    return false;

                Room room = (Room)obj;

            if (!String.IsNullOrWhiteSpace(this.Name) && !String.IsNullOrWhiteSpace(room.Name))
                return this.Name.ToLower() == room.Name.ToLower() && this.Id == room.Id;
            if (String.IsNullOrWhiteSpace(this.Name) && String.IsNullOrWhiteSpace(room.Name))
                return this.Id == room.Id;
            return false;

            //return this.Id == room.Id &&
            //            this.Name == room.Name;

        }

            public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
        #endregion
    }
}
