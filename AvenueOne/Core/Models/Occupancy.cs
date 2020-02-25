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
        private DateTime _date;
        public DateTime Date
        {
            get { return _date; }
            set { _date = value;
                OnPropertyChanged();
                if(value != null && Room != null)
                    this.Booking = GetBookingForDate(Date);
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
                        this.Booking = GetBookingForDate(Date);
                        this.Room.Bookings.CollectionChanged += OnCollectionChanged;
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
                OnPropertyChanged();
            }
        }
        public RoomStatus RoomStatus
        {
            get { return Room.GetRoomStatus(Date); }
        }
        public Occupancy(DateTime date, Room room)
        {
            if (date == null || room == null)
                    throw new ArgumentNullException("to determine occupancy date or room cannot be null.");

            this.Date = date;
            this.Room = room;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] String property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void PropertyHasChanged(object sender, PropertyChangedEventArgs e)
        {
            if( e.PropertyName == nameof(Room.Bookings))
            {
                this.Room.Bookings.CollectionChanged += OnCollectionChanged;
            }
        }

        public void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Booking = GetBookingForDate(Date);
        }

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
    }
}
