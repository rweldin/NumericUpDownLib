﻿namespace UpDownDemoLib.Demos.ViewModels
{
    using UpDownDemoLib.ViewModels;

    /// <summary>
    /// Implements an <see cref="decimal"/> based demo viewmodel that can be used
    /// to drive an integer base numeric up down control.
    /// </summary>
    public class DecimalUpDownViewModel : BaseUpDownViewModel<decimal>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="value"></param>
        /// <param name="minimumValue"></param>
        /// <param name="maximumValue"></param>
        /// <param name="stepSize"></param>
        /// <param name="largeStepSize"></param>
        public DecimalUpDownViewModel(decimal value,
                                      decimal minimumValue,
                                      decimal maximumValue,
                                      decimal stepSize,
                                      decimal largeStepSize
            )
            : base()
        {
            base.Value = value;
            base.MinimumValue = minimumValue;
            base.MaximumValue = maximumValue;
            base.StepSize = stepSize;
            base.LargeStepSize = largeStepSize;
        }

        /// <summary>
        /// Method determine whether two objects of type {T} are equal.
        /// 
        /// Returns false if both objects are in-equal, otherwise true.
        /// </summary>
        /// <param name="intValue"></param>
        /// <param name="intValue1"></param>
        /// <returns></returns>
        public override bool Compare(decimal intValue, decimal intValue1)
        {
            return decimal.Equals(intValue, intValue1);
        }
    }
}
