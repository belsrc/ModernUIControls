// -------------------------------------------------------------------------------
//    SearchBox.cs
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
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Represents a Modern UI search control.
    /// </summary>
    public class SearchBox : ComboBox {
        /// <summary>
        /// Initializes a new instance of the SearchButton class.
        /// </summary>
        public SearchBox()
            : base() {
            this.Style = FindResource( "SearchBox" ) as Style;
        }

        /* Event
           ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Routed event register for the SearchClick event.
        /// </summary>
        public static readonly RoutedEvent SearchClickEvent = EventManager.RegisterRoutedEvent( "SearchClick",
                                                                                                RoutingStrategy.Bubble,
                                                                                                typeof( RoutedEventHandler ),
                                                                                                typeof( SearchBox )
                                                                                              );

        /// <summary>
        /// Adds or removes the event handler for the TabCloseClick event.
        /// </summary>
        public event RoutedEventHandler SearchClick {
            add { AddHandler( SearchClickEvent, value ); }
            remove { RemoveHandler( SearchClickEvent, value ); }
        }


        /* Properties
           ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Dependency Property for SearchHolderText.
        /// </summary>
        public static readonly DependencyProperty SearchHolderTextProperty = DependencyProperty.Register( "SearchHolderText",
                                                                                                         typeof( string ),
                                                                                                         typeof( SearchBox ),
                                                                                                         new PropertyMetadata( string.Empty )
                                                                                                       );

        /// <summary>
        /// Gets or sets the placeholder text value.
        /// </summary>
        public string SearchHolderText {
            get { return ( string )GetValue( SearchHolderTextProperty ); }
            set { SetValue( SearchHolderTextProperty, value ); }
        }


        /* Event Handlers
           ---------------------------------------------------------------------------------------*/

        // Event handler for the search button click event.
        // Adds the search term to the collection.
        // Clears the search box text.
        // Raises the routed SearchClick event.
        private void searchButton_Click( object sender, System.Windows.RoutedEventArgs e ) {
            this.Items.Add( new Label() { Content = this.Text } );
            this.Text = string.Empty;
            this.RaiseEvent( new RoutedEventArgs( SearchClickEvent, this ) );
        }

        /* Methods
           ---------------------------------------------------------------------------------------*/

        /// <summary>
        /// Invoked whenever application code or internal processes call ApplyTemplate.
        /// </summary>
        /// <remarks>
        /// Gets the button from the template and sdds the click event handler to the tab close button.
        /// </remarks>
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            Button searchButton = base.GetTemplateChild( "BtnSearch" ) as Button;
            if( searchButton != null )
                searchButton.Click += new RoutedEventHandler( searchButton_Click );
        }
    }
}
