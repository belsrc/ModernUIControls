// -------------------------------------------------------------------------------
//    WinStateButtons.xaml.cs
//    Copyright (c) 2012 Bryan Kizer
//    All rights reserved.
//    https://github.com/belsrc/ModernUIControls
//
//    Redistribution and use in source and binary forms, with or without
//    modification, are permitted provided that the following conditions are
//    met:
//
//    Redistributions of source code must retain the above copyright notice,
//    this list of conditions and the following disclaimer.
//
//    Redistributions in binary form must reproduce the above copyright notice,
//    this list of conditions and the following disclaimer in the documentation
//    and/or other materials provided with the distribution.
//
//    Neither the name of the Organization nor the names of its contributors
//    may be used to endorse or promote products derived from this software
//    without specific prior written permission.
//
//    THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS
//    IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED
//    TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
//    PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
//    HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
//    SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
//    TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
//    PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
//    LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
//    NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//    SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// -------------------------------------------------------------------------------
namespace ModernUIControls.Controls {
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Interaction logic for WinStateButtons.xaml
    /// </summary>
    /// <remarks>
    /// Public Control Properties:
    ///     IsCloseVisible: bool
    ///     IsMinimizeVisible: bool
    ///     IsMaximizeVisible: bool
    ///     ForegroundHover: Brush
    ///     BackgroundHover: Brush
    /// </remarks>
    public partial class WinStateButtons : UserControl {
        /// <summary>
        /// Initializes a new instance of the WinStateButtons class.
        /// </summary>
        public WinStateButtons() {
            InitializeComponent();
        }

        /* Properties
        ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Dependency Property for ForegroundHover.
        /// </summary>
        public static readonly DependencyProperty ForegroundHoverProperty = DependencyProperty.Register( "ForegroundHover",
                                                                                                         typeof( Brush ),
                                                                                                         typeof( WinStateButtons ),
                                                                                                         new PropertyMetadata( new SolidColorBrush(
                                                                                                                Color.FromArgb( 255, 51, 51, 51 ) ) )
                                                                                                       );

        /// <summary>
        /// Gets or sets the foreground hover brush color.
        /// </summary>
        public Brush ForegroundHover {
            get { return ( Brush )GetValue( ForegroundHoverProperty ); }
            set { SetValue( ForegroundHoverProperty, value ); }
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty BackgroundHoverProperty = DependencyProperty.Register( "BackgroundHover",
                                                                                                         typeof( Brush ),
                                                                                                         typeof( WinStateButtons ),
                                                                                                         new PropertyMetadata( new SolidColorBrush(
                                                                                                                Color.FromArgb( 255, 200, 200, 200 ) ) )
                                                                                                       );
        /// <summary>
        /// 
        /// </summary>
        public Brush BackgroundHover {
            get { return ( Brush )GetValue( BackgroundHoverProperty ); }
            set { SetValue( BackgroundHoverProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for IsCloseVisible.
        /// </summary>
        public static readonly DependencyProperty IsCloseVisibleProperty = DependencyProperty.Register( "IsCloseVisible",
                                                                                                        typeof( Visibility ),
                                                                                                        typeof( WinStateButtons ),
                                                                                                        new PropertyMetadata( Visibility.Visible )
                                                                                                      );

        /// <summary>
        /// Gets or sets the visibility of the close button.
        /// </summary>
        public Visibility IsCloseVisible {
            get { return ( Visibility )GetValue( IsCloseVisibleProperty ); }
            set { SetValue( IsCloseVisibleProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for IsMinimizeVisible.
        /// </summary>
        public static readonly DependencyProperty IsMinimizeVisibleProperty = DependencyProperty.Register( "IsMinimizeVisible",
                                                                                                        typeof( Visibility ),
                                                                                                        typeof( WinStateButtons ),
                                                                                                        new PropertyMetadata( Visibility.Visible )
                                                                                                      );

        /// <summary>
        /// Gets or sets the visibility of the minimize button.
        /// </summary>
        public Visibility IsMinimizeVisible {
            get { return ( Visibility )GetValue( IsMinimizeVisibleProperty ); }
            set { SetValue( IsMinimizeVisibleProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for IsMaximizeVisible.
        /// </summary>
        public static readonly DependencyProperty IsMaximizeVisibleProperty = DependencyProperty.Register( "IsMaximizeVisible",
                                                                                                        typeof( Visibility ),
                                                                                                        typeof( WinStateButtons ),
                                                                                                        new PropertyMetadata( Visibility.Visible )
                                                                                                      );

        /// <summary>
        /// Gets or sets the visibility of the maximize button.
        /// </summary>
        public Visibility IsMaximizeVisible {
            get { return ( Visibility )GetValue( IsMaximizeVisibleProperty ); }
            set { SetValue( IsMaximizeVisibleProperty, value ); }
        }

        /* Event Handlers
           ---------------------------------------------------------------------------------------*/

        // Event handler for the minimize/close button Mouse Enter event. 
        // Fades the background color and foreground color of the canvas.
        private void SvgButton_MouseEnter( object sender, MouseEventArgs e ) {
            Canvas can = sender as Canvas;

            // Check if we can animate the foreground color
            if( this.Foreground.GetType() == typeof( SolidColorBrush ) &&
                ForegroundHover.GetType() == typeof( SolidColorBrush ) ) {
                SolidColorBrush fillBrush = ( can.Children[ 0 ] as Path ).Fill as SolidColorBrush;

                ( can.Children[ 0 ] as Path ).Fill = EaseBrush(
                                                            fillBrush,
                                                            ( ForegroundHover as SolidColorBrush ).Color,
                                                            new Duration( TimeSpan.FromSeconds( .2 ) )
                                                        );
            }

            // Check if we can animate the background color
            if( this.Background.GetType() == typeof( SolidColorBrush ) &&
                BackgroundHover.GetType() == typeof( SolidColorBrush ) ) {
                SolidColorBrush bgBrush = can.Background as SolidColorBrush;

                can.Background = EaseBrush(
                                        bgBrush,
                                        ( BackgroundHover as SolidColorBrush ).Color,
                                        new Duration( TimeSpan.FromSeconds( .2 ) )
                                    );
            }
        }

        // Event handler for the minimize/close button Mouse Leave event. 
        // Fades the background color and foreground color of the canvas.
        private void SvgButton_MouseLeave( object sender, MouseEventArgs e ) {
            Canvas can = sender as Canvas;

            // Check if we can animate the foreground color
            if( this.Foreground.GetType() == typeof( SolidColorBrush ) &&
                ForegroundHover.GetType() == typeof( SolidColorBrush ) ) {

                SolidColorBrush fillBrush = ( can.Children[ 0 ] as Path ).Fill as SolidColorBrush;

                ( can.Children[ 0 ] as Path ).Fill = EaseBrush(
                                                            fillBrush,
                                                            ( this.Foreground as SolidColorBrush ).Color,
                                                            new Duration( TimeSpan.FromSeconds( .2 ) )
                                                        );
            }

            // Check if we can animate the background color
            if( this.Background.GetType() == typeof( SolidColorBrush ) &&
                BackgroundHover.GetType() == typeof( SolidColorBrush ) ) {
                SolidColorBrush bgBrush = ( sender as Canvas ).Background as SolidColorBrush;

                can.Background = EaseBrush(
                                        bgBrush,
                                        ( this.Background as SolidColorBrush ).Color,
                                        new Duration( TimeSpan.FromSeconds( .2 ) )
                                    );
            }
        }

        // Event handler for the Close button click event.
        // Exit the application.
        private void CloseSvg_MouseLeftButtonDown( object sender, MouseButtonEventArgs e ) {
            Application.Current.MainWindow.Close();
        }

        // Event handler for the Maximize button click event.
        // If the window state is normal, maximizes the window. Otherwise, sets the state normal.
        private void MaxSvg_MouseLeftButtonDown( object sender, MouseButtonEventArgs e ) {
            if( Application.Current.MainWindow.WindowState == WindowState.Normal ) {
                Application.Current.MainWindow.WindowState = WindowState.Maximized;
            }
            else {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        // Event handler for the Minimize button click event.
        // Change the WindowState to Minimized.
        private void MiniSvg_MouseLeftButtonDown( object sender, MouseButtonEventArgs e ) {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        /* Methods
           ---------------------------------------------------------------------------------------*/

        // Animates a brush to gradually fade from one color to another.
        private SolidColorBrush EaseBrush( SolidColorBrush brush, Color finish, Duration time ) {
            ColorAnimation ca = new ColorAnimation {
                To = finish,
                Duration = time
            };

            // Since the Color property is sealed create a new SolidBrush with the originals Color
            var tmp = new SolidColorBrush( brush.Color );
            tmp.BeginAnimation( SolidColorBrush.ColorProperty, ca );

            return tmp;
        }
    }
}
