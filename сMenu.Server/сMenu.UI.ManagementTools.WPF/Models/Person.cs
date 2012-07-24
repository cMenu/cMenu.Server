using System;
using WpfApplication1.Helpers;
using System.Windows.Media;

namespace iMenu.UI.ManagementTools.WPF.Models
{
    public class Person : NotificationObject
    {
        #region Ctor
        public Person(string name, int age, bool isMarried, double height, DateTime birthDate, Color favColor)
        {
            Name = name;
            Age = age;
            IsMarried = isMarried;
            Height = height;
            BirthDate = birthDate;
            FavoriteColor = new SolidColorBrush(favColor);
        }
        #endregion

        #region Name

        private string _Name;
        public string Name
        {
            get { return this._Name; }
            set
            {
                if (this._Name != value)
                {
                    this._Name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }

        #endregion

        #region Age

        private int _Age;
        public int Age
        {
            get { return this._Age; }
            set
            {
                if (this._Age != value)
                {
                    this._Age = value;
                    RaisePropertyChanged(() => Age);
                }
            }
        }

        #endregion

        #region IsMarried

        private bool _IsMarried;
        public bool IsMarried
        {
            get { return this._IsMarried; }
            set
            {
                if (this._IsMarried != value)
                {
                    this._IsMarried = value;
                    RaisePropertyChanged(() => IsMarried);
                }
            }
        }

        #endregion

        #region Height

        private double _Height;
        public double Height
        {
            get { return this._Height; }
            set
            {
                if (this._Height != value)
                {
                    this._Height = value;
                    RaisePropertyChanged(() => Height);
                }
            }
        }

        #endregion

        #region BirthDate

        private DateTime _BirthDate;
        public DateTime BirthDate
        {
            get { return this._BirthDate; }
            set
            {
                if (this._BirthDate != value)
                {
                    this._BirthDate = value;
                    RaisePropertyChanged(() => BirthDate);
                }
            }
        }

        #endregion

        #region FavoriteColor

        private SolidColorBrush _FavoriteColor;
        public SolidColorBrush FavoriteColor
        {
            get { return this._FavoriteColor; }
            set
            {
                if (this._FavoriteColor != value)
                {
                    this._FavoriteColor = value;
                    RaisePropertyChanged(() => FavoriteColor);
                }
            }
        }

        #endregion

    }
}
