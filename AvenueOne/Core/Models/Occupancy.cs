using AvenueOne.Core.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AvenueOne.Core.Models
{
    public class Occupancy : INotifyPropertyChanged
    {

        #region Properties

            private DateTime _date;
            public DateTime Date
            {
                get { return _date; }
                set { _date = value;
                    OnPropertyChanged();
                    if(value != null && Room != null)
                        this.Booking = GetBookingForDate(Date);
                    OnPropertyChanged(nameof(RoomStatus));
            }
        }

            private Room _room;
            public Room Room
            {
                get { return _room; }
                set { _room = value;
                    OnPropertyChanged();
                    if(value != null)
                    {
                        if(value.Bookings != null)
                        {
                            this.Room.Bookings.CollectionChanged += OnCollectionChanged;
                            this.Booking = GetBookingForDate(Date);
                        }
                        else
                        {
                            this.Room.PropertyChanged += PropertyHasChanged;
                        }
                        OnPropertyChanged(nameof(RoomStatus));
                    }

                }
            }

            private Booking _booking;
            public Booking Booking
            {
                get { return _booking; }
                set { _booking = value;
                    if (value != null)
                        Booking.PropertyChanged += PropertyHasChanged;
                OnPropertyChanged();
                OnPropertyChanged(nameof(RoomStatus));
            }
            }
            public RoomStatus RoomStatus
            {
                get { return Room.GetRoomStatus(Date); }
            }
        #endregion

        #region Constructors

            public Occupancy(DateTime date, Room room)
            {
                if (date == null || room == null)
                        throw new ArgumentNullException("to determine occupancy date or room cannot be null.");

                this.Date = date;
                this.Room = room;
                //this.Room.Bookings.CollectionChanged += OnCollectionChanged;
            }


        #endregion

        #region PropertyChanged

            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged([CallerMemberName] String property = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
            }

            public void PropertyHasChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(Room.Bookings))
                {
                    this.Room.Bookings.CollectionChanged += OnCollectionChanged;
                }
            if (e.PropertyName == nameof(Booking.Status))
                OnPropertyChanged(nameof(RoomStatus));
                //this.Booking = GetBookingForDate(Date);
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Booking = GetBookingForDate(Date);
        }
        #endregion


        #region Methods
            private Booking GetBookingForDate(DateTime date)
            {
                if (date == null)
                    return null;
                if (Room == null)
                    return null;
                if (Room.Bookings == null)
                    return null;
                return this.Room.Bookings.Where(b => b.IsDateBetweenBookingDate(date)).FirstOrDefault();
            }

        #endregion

        #region Static Methods
        public static List<Occupancy> GenerateOccupancyList(ICollection<Room> rooms, DateTime dateTime)
        {
            List<Occupancy> occupancyList = new List<Occupancy>();
            foreach (var room in rooms)
            {
                        var occupancy = new Occupancy(dateTime, room);
                        //var occupancy = new Occupancy(date, room.GetRoomStatus(date));
                    occupancyList.Add(occupancy);
            }
            return occupancyList;
        }

        public static Dictionary<Room, List<Occupancy>> GenerateOccupancyDictionaryMonth(ICollection<Room> rooms, DateTime currentDate)
        {
            Dictionary<Room, List<Occupancy>> occupancyList = new Dictionary<Room, List<Occupancy>>();
            foreach (var room in rooms)
            {
                if (!occupancyList.ContainsKey(room))
                {
                    List<DateTime> getDatesInAMonth = GetDatesInAMonth(currentDate);
                    List<Occupancy> availabilityList = new List<Occupancy>();

                    foreach (var date in getDatesInAMonth)
                    {
                        var occupancy = new Occupancy(date, room);
                        //var occupancy = new Occupancy(date, room.GetRoomStatus(date));
                        availabilityList.Add(occupancy);
                    }
                    occupancyList.Add(room, availabilityList);
                }
            }

            return occupancyList;
        }

        private static List<DateTime> GetDatesInAMonth(DateTime date)
        {
            List<DateTime> datesInAMonth = new List<DateTime>();
            int year = date.Year;
            int month = date.Month;
            int daysInAMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= daysInAMonth; i++)
            {
                datesInAMonth.Add(new DateTime(year, month, i));
            }

            return datesInAMonth;
        }
        #endregion
    }
}
