// -------------------------------------------------------------------------------
//    SlideCheck.xaml.cs
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

    /// <summary>
    /// Interaction logic for SlideCheck.xaml
    /// </summary>
    /// <remarks>
    /// Public Control Properties:
    ///     IsChecked: bool
    ///     CheckedBrush: Brush
    ///     UncheckedBrush: Brush
    ///     ThumbBrush: Brush
    ///
    /// Public Event
    ///     CheckChanged
    /// </remarks>
    public partial class SlideCheck : UserControl {
        /* Constructors
           ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Initializes a new instance of the SlideCheck class.
        /// </summary>
        public SlideCheck() {
            InitializeComponent();
        }

        /* Event
           ---------------------------------------------------------------------------------------*/

        // Occurs when the control is clicked and the IsChecked properties changes.
        public event EventHandler CheckChanged;

        /* Properties
           ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Dependency Property for CheckedColor.
        /// </summary>
        public static readonly DependencyProperty CheckedBrushProperty =
                                                  DependencyProperty.Register( "CheckedBrush",              // name of the dependency property to register
                                                                               typeof( Brush ),             // type of the property
                                                                               typeof( SlideCheck ),        // owner type that is registering the property
                                                                               new PropertyMetadata(        // metadata (default value) for the property
                                                                                   new SolidColorBrush(
                                                                                       Color.FromArgb( 255, 27, 161, 226 )
                                                                                   )
                                                                               )
                                                                             );

        /// <summary>
        /// Gets or sets the fill color for the Checked path.
        /// </summary>
        public Brush CheckedBrush {
            get { return ( Brush )GetValue( CheckedBrushProperty ); }
            set { SetValue( CheckedBrushProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for UncheckedColor.
        /// </summary>
        public static readonly DependencyProperty UncheckedBrushProperty =
                                                  DependencyProperty.Register( "UncheckedBrush",            // name of the dependency property to register
                                                                               typeof( Brush ),             // type of the property
                                                                               typeof( SlideCheck ),        // owner type that is registering the property
                                                                               new PropertyMetadata(        // metadata (default value) for the property
                                                                                   new SolidColorBrush(
                                                                                       Color.FromArgb( 255, 125, 125, 125 )
                                                                                   )
                                                                               )
                                                                             );

        /// <summary>
        /// Gets or sets the fill color for the Unchecked path.
        /// </summary>
        public Brush UncheckedBrush {
            get { return ( Brush )GetValue( UncheckedBrushProperty ); }
            set { SetValue( UncheckedBrushProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for SliderColor.
        /// </summary>
        public static readonly DependencyProperty ThumbBrushProperty =
                                                  DependencyProperty.Register( "ThumbBrush",               // name of the dependency property to register
                                                                               typeof( Brush ),             // type of the property
                                                                               typeof( SlideCheck ),        // owner type that is registering the property
                                                                               new PropertyMetadata(        // metadata (default value) for the property
                                                                                   new SolidColorBrush(
                                                                                       Color.FromArgb( 255, 51, 51, 51 )
                                                                                   )
                                                                               )
                                                                             );

        /// <summary>
        /// Gets or sets the fill color for the Slider path.
        /// </summary>
        public Brush ThumbBrush {
            get { return ( Brush )GetValue( ThumbBrushProperty ); }
            set { SetValue( ThumbBrushProperty, value ); }
        }

        /// <summary>
        /// Dependency Property for IsChecked.
        /// </summary>
        public static readonly DependencyProperty IsCheckedProperty =
                                                  DependencyProperty.Register( "IsChecked",                  // name of the dependency property to register
                                                                               typeof( bool ),               // type of the property
                                                                               typeof( SlideCheck ),         // owner type that is registering the property
                                                                               new PropertyMetadata( false,  // metadata (default value) for the property
                                                                                   new PropertyChangedCallback( CheckedValueChanged ) )
                                                                             );

        /// <summary>
        /// Gets or sets the user controls IsChecked property.
        /// </summary>
        public bool IsChecked {
            get { return ( Boolean )GetValue( IsCheckedProperty ); }
            set { SetValue( IsCheckedProperty, value ); }
        }

        /* Event Handlers
           ---------------------------------------------------------------------------------------*/

        // Event handler for the IsCheckedProperty value changed event.
        private static void CheckedValueChanged( DependencyObject obj, DependencyPropertyChangedEventArgs args ) {
            SlideCheck sc = ( obj as SlideCheck );
            if( sc.IsChecked ) {
                sc.AnimateSlider( 0 );
            }
            else {
                sc.AnimateSlider( -46 );
            }

            if( sc.CheckChanged != null ) {
                sc.CheckChanged.Invoke( sc, EventArgs.Empty );
            }
        }

        // Event handler for the control Click event.
        private void SlideCheckControl_MouseLeftButtonDown( object sender, MouseButtonEventArgs e ) {
            IsChecked = !IsChecked;
        }

        // Event handler for the Loaded event.
        // If checked, sets the slider to the true position, skipping the animation.
        private void SlideCheckControl_Loaded( object sender, RoutedEventArgs e ) {
            if( this.IsChecked ) {
                this.slideSvg.Margin = new Thickness( 1, 0, 0, 0 );
            }
        }

        /* Methods
           ---------------------------------------------------------------------------------------*/

        // Animates the slider from one side to other.
        private void AnimateSlider( double position ) {
            ThicknessAnimation ta = new ThicknessAnimation {
                To = new Thickness( position, 0, 0, 0 ),
                Duration = new Duration( TimeSpan.FromSeconds( .3 ) )
            };

            this.slideSvg.BeginAnimation( MarginProperty, ta );
        }
    }
}