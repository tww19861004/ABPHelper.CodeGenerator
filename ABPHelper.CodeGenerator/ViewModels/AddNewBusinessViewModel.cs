﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ABPHelper.CodeGenerator.Extensions;
using ABPHelper.CodeGenerator.Helper;
using ABPHelper.CodeGenerator.Models.HelperModels;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace ABPHelper.CodeGenerator.ViewModels
{
    public class AddNewBusinessViewModel : ViewModelBase
    {
        public class ViewFileViewModel : ObservableObject
        {
            #region FileName 属性

            private string _fileName;

            /// <summary>
            /// 设置或取得 FileName 属性
            /// 更改属性值并引发PropertyChanged事件
            /// </summary>
            public string FileName
            {
                get => _fileName;

                set
                {
                    if (Set(ref _fileName, value))
                    {
                    }
                }
            }

            #endregion

            #region IsPopup 属性

            private bool _isPopup;

            /// <summary>
            /// 设置或取得 IsPopup 属性
            /// 更改属性值并引发PropertyChanged事件
            /// </summary>
            public bool IsPopup
            {
                get => _isPopup;

                set
                {
                    if (Set(ref _isPopup, value))
                    {
                    }
                }
            }

            #endregion
        }

        public AddNewBusinessViewModel()
        {
#if DEBUG
            BusinessName = "Contract";
#endif
            ViewFiles = new ObservableCollection<ViewFileViewModel>(new List<ViewFileViewModel>
            {
                new ViewFileViewModel {FileName = "Index", IsPopup = false},
                new ViewFileViewModel {FileName = "CreateOrUpdateModal", IsPopup = true},
            });
        }

        #region BusinessName 属性

        private string _businessName;

        /// <summary>
        /// 设置或取得 BusinessName 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public string BusinessName
        {
            get => _businessName;

            set
            {
                if (Set(ref _businessName, value))
                {
                    ServiceFolder = BusinessName + "s";
                    ServiceInterfaceName = $"I{BusinessName}AppService";
                    ServiceName = $"{BusinessName}AppService";
                    ViewFolder = $@"App\Main\views\{ServiceFolder.LowerFirstChar()}";
                }
            }
        }

        #endregion

        #region ServiceFolder 属性

        private string _serviceFolder;

        /// <summary>
        /// 设置或取得 ServiceFolder 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public string ServiceFolder
        {
            get => _serviceFolder;

            set
            {
                if (Set(ref _serviceFolder, value))
                {
                }
            }
        }

        #endregion

        #region ServiceInterfaceName 属性

        private string _serviceInterfaceName;

        /// <summary>
        /// 设置或取得 ServiceInterfaceName 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public string ServiceInterfaceName
        {
            get => _serviceInterfaceName;

            set
            {
                if (Set(ref _serviceInterfaceName, value))
                {
                }
            }
        }

        #endregion

        #region ServiceName 属性

        private string _serviceName;

        /// <summary>
        /// 设置或取得 ServiceName 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public string ServiceName
        {
            get => _serviceName;

            set
            {
                if (Set(ref _serviceName, value))
                {
                }
            }
        }

        #endregion

        #region ViewFolder 属性

        private string _viewFolder;

        /// <summary>
        /// 设置或取得 ViewFolder 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public string ViewFolder
        {
            get => _viewFolder;

            set
            {
                if (Set(ref _viewFolder, value))
                {
                }
            }
        }

        #endregion

        #region ViewFiles 属性

        private ObservableCollection<ViewFileViewModel> _viewFiles;

        /// <summary>
        /// 设置或取得 ViewFiles 属性
        /// 更改属性值并引发PropertyChanged事件
        /// </summary>
        public ObservableCollection<ViewFileViewModel> ViewFiles
        {
            get => _viewFiles;

            set
            {
                if (Set(ref _viewFiles, value))
                {
                }
            }
        }

        #endregion

        #region GenerateCommand 命令 

        private RelayCommand _generateCommand;

        /// <summary>
        /// 取得 GenerateCommand。
        /// </summary>
        public RelayCommand GenerateCommand => _generateCommand ?? (_generateCommand = new RelayCommand(
            ExecuteGenerateCommand,
            CanExecuteGenerateCommand));

        private void ExecuteGenerateCommand()
        {
            if (Utils.MessageBox("Are you sure to generate?", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.Cancel)
            {
                return;
            }

            var helper = new AddNewBusinessHelper(AbpHelperWindowControl.ServiceProvider);
            var parameter = new AddNewBusinessModel()
            {
                BusinessName = BusinessName,
                ServiceFolder = ServiceFolder,
                ServiceInterfaceName = ServiceInterfaceName,
                ServiceName = ServiceName,
                ViewFolder = ViewFolder,
                ViewFiles = ViewFiles,
            };
            if (helper.CanExecute(parameter))
            {
                helper.Execute(parameter);
            }
        }

        private bool CanExecuteGenerateCommand()
        {
            return !string.IsNullOrEmpty(BusinessName) &&
                   !string.IsNullOrEmpty(ServiceFolder) &&
                   !string.IsNullOrEmpty(ServiceInterfaceName) &&
                   !string.IsNullOrEmpty(ServiceName);
        }

        #endregion

    }
}