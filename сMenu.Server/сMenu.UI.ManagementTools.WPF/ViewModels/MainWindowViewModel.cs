using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace iMenu.UI.ManagementTools.WPF.Helpers;
namespace iMenu.UI.ManagementTools.WPF.Models;
namespace iMenu.UI.ManagementTools.WPF.ViewModels;
using System.Windows.Media;

namespace iMenu.UI.ManagementTools.WPF.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Properties

        #region MyDateTime

        private DateTime _MyDateTime;
        public DateTime MyDateTime
        {
            get { return this._MyDateTime; }
            set
            {
                if (this._MyDateTime != value)
                {
                    this._MyDateTime = value;
                    RaisePropertyChanged(() => MyDateTime);
                }
            }
        }

        #endregion

        #region PersonsCollection

        private ObservableCollection<Person> _PersonsCollection;
        public ObservableCollection<Person> PersonsCollection
        {
            get { return this._PersonsCollection; }
            set
            {
                if (this._PersonsCollection != value)
                {
                    this._PersonsCollection = value;
                    RaisePropertyChanged(() => PersonsCollection);
                }
            }
        }

        #endregion

        #endregion

        #region Commands

        public ICommand RefreshDateCommand { get { return new DelegateCommand(OnRefreshDate); } }
        public ICommand RefreshPersonsCommand { get { return new DelegateCommand(OnRefreshPersons); } }
        public ICommand DoNothingCommand { get { return new DelegateCommand(OnDoNothing, CanExecuteDoNothing); } }

        #endregion

        #region Ctor
        public MainWindowViewModel()
        {
            RandomizeData();
        }
        #endregion

        #region Command Handlers

        private void OnRefreshDate()
        {
            MyDateTime = DateTime.Now;
        }

        private void OnRefreshPersons()
        {
            RandomizeData();
        }

        private void OnDoNothing()
        {
        }

        private bool CanExecuteDoNothing()
        {
            return false;
        }

        #endregion

        private void RandomizeData()
        {
            PersonsCollection = new ObservableCollection<Person>();

            for (int i = 0; i < 10; i++)
            {
                PersonsCollection.Add(new Person(
                    RandomHelper.RandomString(10, true),
                    RandomHelper.RandomInt(1, 43),
                    RandomHelper.RandomBool(),
                    RandomHelper.RandomNumber(50, 180, 1),
                    RandomHelper.RandomDate(new DateTime(1980, 1, 1), DateTime.Now),
                    RandomHelper.RandomColor()
                    ));
            }
        }

    }
}