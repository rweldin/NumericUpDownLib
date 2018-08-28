﻿namespace NumericUpDownLib
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    /// <summary>
    /// This class serve as target for styling the <see cref="AbstractBaseUpDown{T}"/> class
    /// since styling directly on <see cref="AbstractBaseUpDown{T}"/> is not supported in XAML.
    /// </summary>
    public abstract class InputBaseUpDown : Control
    {
        #region fields
        /// <summary>
        /// Determines whether the textbox portion of the control is editable
        /// (requires additional check of bounds) or not.
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly",
                typeof(bool), typeof(InputBaseUpDown), new PropertyMetadata(true));

        /// <summary>
        /// Determines the allowed style of a number entered and displayed in the textbox.
        /// </summary>
        public static readonly DependencyProperty NumberStyleProperty =
            DependencyProperty.Register("NumberStyle", typeof(NumberStyles),
                typeof(InputBaseUpDown), new PropertyMetadata(NumberStyles.Any));

        private static RoutedCommand mIncreaseCommand;
        private static RoutedCommand mDecreaseCommand;
        #endregion fields

        /// <summary>
        /// Class constructor
        /// </summary>
        public InputBaseUpDown()
        {
            InitializeCommands();
        }

        #region properties
        /// <summary>
        /// Expose the increase value command via <seealso cref="RoutedCommand"/> property.
        /// </summary>
        public static RoutedCommand IncreaseCommand
        {
            get
            {
                return mIncreaseCommand;
            }
        }

        /// <summary>
        /// Expose the decrease value command via <seealso cref="RoutedCommand"/> property.
        /// </summary>
        public static RoutedCommand DecreaseCommand
        {
            get
            {
                return mDecreaseCommand;
            }
        }

        /// <summary>
        /// Determines whether the textbox portion of the control is editable
        /// (requires additional check of bounds) or not.
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }

        /// <summary>
        /// Gets/sets the allowed style of a number entered and displayed in the textbox.
        /// </summary>
        public NumberStyles NumberStyle
        {
            get { return (NumberStyles)GetValue(NumberStyleProperty); }
            set { SetValue(NumberStyleProperty, value); }
        }
        #endregion properties

        #region methods
        /// <summary>
        /// User can mouse over the control and spin the mousewheel up or down
        /// to increment or decrement the value in the up/down control.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Handled == false )
            {
                if (e.Delta != 0)
                {
                    if (e.Delta < 0 && CanDecreaseCommand() == true)
                    {
                        OnDecrease();

                        e.Handled = true;
                    }
                    else
                    {
                        if (e.Delta > 0 && CanIncreaseCommand() == true)
                        {
                            OnIncrease();

                            e.Handled = true;
                        }
                    }
                }
            }
        }

        #region Commands
        /// <summary>
        /// Increase the displayed integer value
        /// </summary>
        protected abstract void OnIncrease();

        /// <summary>
        /// Determines whether the increase command is available or not.
        /// </summary>
        protected abstract bool CanIncreaseCommand();

        /// <summary>
        /// Decrease the displayed integer value
        /// </summary>
        protected abstract void OnDecrease();

        /// <summary>
        /// Determines whether the decrease command is available or not.
        /// </summary>
        protected abstract bool CanDecreaseCommand();

        /// <summary>
        /// Initialize up down/button commands and key gestures for up/down cursor keys
        /// </summary>
        private void InitializeCommands()
        {
            InputBaseUpDown.mIncreaseCommand = new RoutedCommand("IncreaseCommand", typeof(InputBaseUpDown));
            CommandManager.RegisterClassCommandBinding(typeof(InputBaseUpDown),
                                    new CommandBinding(mIncreaseCommand, OnIncreaseCommand, OnCanIncreaseCommand));

            CommandManager.RegisterClassInputBinding(typeof(InputBaseUpDown),
                                    new InputBinding(mIncreaseCommand, new KeyGesture(Key.Up)));

            InputBaseUpDown.mDecreaseCommand = new RoutedCommand("DecreaseCommand", typeof(InputBaseUpDown));

            CommandManager.RegisterClassCommandBinding(typeof(InputBaseUpDown),
                                    new CommandBinding(mDecreaseCommand, OnDecreaseCommand, OnDecreaseCommand));

            CommandManager.RegisterClassInputBinding(typeof(InputBaseUpDown),
                                    new InputBinding(mDecreaseCommand, new KeyGesture(Key.Down)));
        }

        private static void OnCanIncreaseCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as InputBaseUpDown;
            if (control != null)
            {
                e.CanExecute = control.CanIncreaseCommand();
                e.Handled = true;
            }
        }

        private static void OnDecreaseCommand(object sender, CanExecuteRoutedEventArgs e)
        {
            var control = sender as InputBaseUpDown;
            if (control != null)
            {
                e.CanExecute = control.CanDecreaseCommand();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Execute the increase value command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as InputBaseUpDown;
            if (control != null)
            {
                control.OnIncrease();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Execute the decrease value command
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            var control = sender as InputBaseUpDown;
            if (control != null)
            {
                control.OnDecrease();
                e.Handled = true;
            }
        }
        #endregion
        #endregion methods
    }
}