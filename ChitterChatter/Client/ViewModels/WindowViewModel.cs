using System.Windows;
using System;
using System.Windows.Input;
using System.Runtime.InteropServices;

using TCPHelpers.BaseClasses.Client;
using Client.AppModel;
using Client.WindowResizer;
using Constants.Views;

namespace Client.ViewModels
{
    /// <summary>
    /// The View Model for the custom flat window
    /// </summary>
    /// <seealso cref="TCPHelpers.BaseClasses.Client.BaseViewModel" />
    public class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The client inside the window view model
        /// </summary>
        private ClientAPPMODEL mClient = null;

        /// <summary>
        /// The window this view model controls
        /// </summary>
        private Window mWindow = null;

        /// <summary>
        /// The window resizer to fix window resizing issues
        /// </summary>
        private WindowResizer.WindowResizer mWindowResizer = null;

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

        /// <summary>
        /// The height of the title bar  / caption of the window
        /// </summary>
        private int mTitleHeight = 42;

        /// <summary>
        /// The maximize icon
        /// </summary>
        private string mMaximizeIcon = "▯";

        /// <summary>
        /// The last known dock position
        /// </summary>
        private WindowDockPosition mDockPosition = WindowDockPosition.Undocked;

        #endregion


        #region Public Properties

        public ClientAPPMODEL Client { get; }

        #region Window minimum height and width
        /// <summary>
        /// The smallest width the window can go to
        /// </summary>
        public double WindowMinimumWidth { get; set; } = 400;

        /// <summary>
        /// The smallest height the window can go to
        /// </summary>
        public double WindowMinimumHeight { get; set; } = 400;
        #endregion

        /// <summary>
        /// True if the window should be borderless because it is docked or maximized
        /// </summary>
        public bool Borderless 
        {
            get
            {
                if (this.mWindow.WindowState == WindowState.Maximized || this.mDockPosition != WindowDockPosition.Undocked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #region Resize Border and thickness properties
        /// <summary>
        /// The size of the resize border around the window
        /// </summary>
        public int ResizeBorder
        {
            get
            {
                if (this.Borderless)
                {
                    return 0;
                }
                else
                {
                    return this.mResizeBorder;
                }
            }
        }

        /// <summary>
        /// The size of the resize border around the window, taking into account the outer margin
        /// </summary>
        public Thickness ResizeBorderThickness
        {
            get
            {
                return new Thickness(this.ResizeBorder + this.OuterMarginSize);
            }
        }
        #endregion

        /// <summary>
        /// The padding if the inner content of the main window
        /// </summary>
        public Thickness InnerContentPadding { get; set; } = new Thickness(0);

        #region Outer margin size and thickness properties
        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public int OuterMarginSize 
        {
            get
            {
                if (this.Borderless)
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
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The margin around the window to allow for a drop shadow
        /// </summary>
        public Thickness OuterMarginThickness 
        {
            get
            {
                return new Thickness(this.OuterMarginSize);
            } 
        }
        #endregion

        #region Window and corner radius
        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public int WindowRadius
        {
            get
            {
                if (this.Borderless)
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
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The radius of the edges of the window
        /// </summary>
        public CornerRadius WindowCornerRadius
        { 
            get 
            { 
                return new CornerRadius(this.WindowRadius);
            }
        }
        #endregion

        #region Title height
        /// <summary>
        /// The height of the title bar  / caption of the window
        /// </summary>
        public int TitleHeight 
        {
            get
            {
                return this.mTitleHeight;
            }
            set
            {
                this.mTitleHeight = value;
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GridLength TitleHeightGridLength 
        {
            get
            {
                return new GridLength(this.TitleHeight + this.ResizeBorder);
            }
        }
        #endregion

        /// <summary>
        /// The maximize icon
        /// </summary>
        public string MaximizeIcon 
        {
            get
            {
                return this.mMaximizeIcon;
            }
            set
            {
                this.mMaximizeIcon = value;
                base.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// The current page of the application
        /// </summary>
        public ApplicationPage CurrentPage { get; set; } = ApplicationPage.Login;

        #endregion


        #region Commands

        /// <summary>
        /// The command to minimize the window
        /// </summary>
        public ICommand MinimizeCommand { get; set; }

        /// <summary>
        /// The command to maximize the window
        /// </summary>
        public ICommand MaximizeCommand { get; set; }

        /// <summary>
        /// The command to close the window
        /// </summary>
        public ICommand CloseCommand { get; set; }

        /// <summary>
        /// The command to show the system menu of the window
        /// </summary>
        public ICommand MenuCommand { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WindowViewModel(Window window)
        {
            this.mWindow = window;

            // Listen out for the window resizing
            this.mWindow.StateChanged += (sender, e) =>
            {
                this.WindowResized();
            };

            // Create commands
            this.MinimizeCommand = new RelayCommand(() => this.mWindow.WindowState = WindowState.Minimized);
            this.MaximizeCommand = new RelayCommand(() => 
            {
                // User went half screen
                if (this.mWindow.WindowState == WindowState.Maximized)
                {
                    this.mWindow.WindowState = WindowState.Normal;
                    this.MaximizeIcon = "▯";
                }
                // User went full screen
                else if (this.mWindow.WindowState == WindowState.Normal)
                {
                    this.mWindow.WindowState = WindowState.Maximized;
                    this.MaximizeIcon = "❐";
                }
            });
            this.CloseCommand = new RelayCommand(() => this.mWindow.Close());
            this.MenuCommand = new RelayCommand(() => SystemCommands.ShowSystemMenu(this.mWindow, this.GetMousePosition()));

            // Fix the window resize issue
            this.mWindowResizer = new WindowResizer.WindowResizer(this.mWindow);
            this.mWindowResizer.WindowDockChanged += (dock) =>
            {
                // Store last position
                this.mDockPosition = dock;

                // Fire off resize events
                this.WindowResized();
            };
            
            // Set up the client to communicate with the server
            this.mClient = new ClientAPPMODEL();
;
        }

        #endregion


        #region Private Helpers

        /// <summary>
        /// Gets the current mouse position on the screen
        /// </summary>
        /// <returns></returns>
        private Point GetMousePosition()
        {
            // Position of the mouse relative to the window
            var position = Mouse.GetPosition(this.mWindow);

            // Add the window position so its a "ToScreen"
            return new Point(position.X + this.mWindow.Left, position.Y + this.mWindow.Top);
        }

        private void WindowResized()
        {
            // Fire off events for all properties that are affected by a resize
            base.NotifyPropertyChanged(nameof(this.Borderless));
            base.NotifyPropertyChanged(nameof(this.ResizeBorderThickness));
            base.NotifyPropertyChanged(nameof(this.OuterMarginSize));
            base.NotifyPropertyChanged(nameof(this.OuterMarginThickness));
            base.NotifyPropertyChanged(nameof(this.WindowRadius));
            base.NotifyPropertyChanged(nameof(this.WindowCornerRadius));
        }

        #endregion
    }
}
