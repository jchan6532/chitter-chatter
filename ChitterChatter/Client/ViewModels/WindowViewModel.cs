﻿using System.Windows;
using System;

using TCPHelpers.BaseClasses.Client;

namespace Client.ViewModels
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow = null;

        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        private int mResizeBorder = 6;

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        private int mOuterMarginSize = 10;

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        private int mWindowRadius = 10;

        #endregion


        #region Public Properties

        #region Resize Border and thickness properties
        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder
        {
            get
            {
                return this.mResizeBorder;
            }
            set
            {
                this.mResizeBorder = value;
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get
            {
                return new Thickness(this.ResizeBorder);
            }
        }
        #endregion

        #region Outer margin size and thickness properties
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize 
        {
            get
            {
                if (this.mWindow.WindowState == WindowState.Maximized)
                {
                    return 0;
                }
                else
                {
                    return this.mOuterMarginSize;
                }
            }
            set
            {
                this.mOuterMarginSize = value;
            }
        }
        #endregion

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                if (this.mWindow.WindowState == WindowState.Maximized)
                {
                    return 0;
                }
                else
                {
                    return this.mWindowRadius;
                }
            }
            set
            {
                this.mWindowRadius = value;
            }
        }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this.mWindow = window;

            this.ResizeBorder = 6;
        }

        #endregion
    }
}