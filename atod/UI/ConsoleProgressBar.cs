// Copyright 2022 Raising the Floor - US, Inc.
//
// The R&D leading to these results received funding from the:
// * Rehabilitation Services Administration, US Dept. of Education under
//   grant H421A150006 (APCP)
// * National Institute on Disability, Independent Living, and
//   Rehabilitation Research (NIDILRR)
// * Administration for Independent Living & Dept. of Education under grants
//   H133E080022 (RERC-IT) and H133E130028/90RE5003-01-00 (UIITA-RERC)
// * European Union's Seventh Framework Programme (FP7/2007-2013) grant
//   agreement nos. 289016 (Cloud4all) and 610510 (Prosperity4All)
// * William and Flora Hewlett Foundation
// * Ontario Ministry of Research and Innovation
// * Canadian Foundation for Innovation
// * Adobe Foundation
// * Consumer Electronics Association Foundation

using ABI.Windows.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace atod.UI
{
    internal class ConsoleProgressBar
    {
        // value (and min/max) of progress bar
        public double Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                _minimum = value;
                this.RequestUIUpdate();
            }
        }
        private double _minimum = 100.0;
        //
        public double Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                _maximum = value;
                this.RequestUIUpdate();
            }
        }
        private double _maximum = 100.0;
        //
        public double Value
        {
            get 
            { 
                return _value; 
            }
            set
            {
                _value = value;
                this.RequestUIUpdate();
            }
        }
        private double _value = 0.0;
        //
        // width of progress bar
        public int Width 
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                this.RequestUIUpdate();
            }
        }
        private int _width = 30;
        //
        // optional trailing text
        public string? TrailingText
        {
            get
            {
                return _trailingText;
            }
            set
            {
                _trailingText = value;
                this.RequestUIUpdate();
            }
        }
        private string? _trailingText = null;
        //
        public ConsoleColor? TrailingTextBackgroundColor { 
            get
            {
                return _trailingTextBackgroundColor;
            }
            set
            {
                _trailingTextBackgroundColor = value;
                this.RequestUIUpdate();
            }
        }
        private ConsoleColor? _trailingTextBackgroundColor = null;
        //
        public ConsoleColor? TrailingTextForegroundColor {
            get 
            {
                return _trailingTextForegroundColor;
            }
            set
            {
                _trailingTextForegroundColor = value;
                this.RequestUIUpdate();
            }
        }
        private ConsoleColor? _trailingTextForegroundColor = null;

        const char FULL_BLOCK_CHAR = '\u2588';
        const char LIGHT_SHADE_CHAR = '\u2591';
        const char MEDIUM_SHADE_CHAR = '\u2592';
        const char DARK_SHADE_CHAR = '\u2593';

        const int LEADING_PADDING_WIDTH = 2;
        const int TRAILING_PADDING_WIDTH = 2;

        // NOTE: this variable should only be modified by the UpdateUIElement(...) function
        private int _lengthOfLineAtLastUpdate = 0;

        private bool _isVisible;

        public ConsoleProgressBar()
        {
            // NOTE: our ConsoleProgressBar starts out hidden (to allow the caller to configure its properties before showing it for the first time)
            _isVisible = false;
        }

        public void Show()
        {
            if (_isVisible == true)
            {
                return;
            }

            _isVisible = true;

            this.RequestUIUpdate(false /*waitForCompletion*/);
        }

        public void Hide()
        {
            if (_isVisible == false)
            {
                return;
            }

            _isVisible = false;

            this.RequestUIUpdate(true /*waitForCompletion*/);
        }

        // NOTE: in the current implementation, we always wait for completion; in the future, we may choose to make RequestUIUpdate queue up UI update calls (with a minimum interval between calls, preparing an extra iteration on a timer as appropriate)
        private void RequestUIUpdate(bool waitForCompletion = false)
        {
            this.UpdateUIElement();
        }

        private object _updateUIElementLockObject = new object();
        private void UpdateUIElement()
        {
            // NOTE: to ensure that our function's code can complete before being executed again, we use a synchronizing lock object to prevent re-entrancy
            lock (_updateUIElementLockObject)
            {
                // NOTE: we capture all of the element's properties before proceeding with the UI update (and for efficiency) in case they change while we are updating the UI
                var elementMinimum = _minimum;
                var elementMaximum = _maximum;
                var elementValue = _value;
                //
                var elementWidth = this.Width;
                //
                var elementTrailingText = _trailingText;
                var elementTrailingTextForegroundColor = _trailingTextForegroundColor;
                var elementTrailingTextBackgroundColor = _trailingTextBackgroundColor;

                // capture the current console background/foreground colors (so that we can restore them once our rendering is complete)
                var originalBackgroundColor = Console.BackgroundColor;
                var originalForegroundColor = Console.ForegroundColor;

                // reset the cursor to the beginning of the current line
                Console.SetCursorPosition(0, Console.CursorTop);

                if (_isVisible == true)
                {
                    // reset the cursor to the beginning of the current line
                    Console.SetCursorPosition(0, Console.CursorTop);

                    // write out leading spaces
                    Console.Write(new String(' ', LEADING_PADDING_WIDTH));

                    // create current progress bar
                    var progressBarStringBuilder = new StringBuilder(elementWidth);
                    //
                    double percentageFraction;
                    if (elementMaximum - elementMinimum != 0.0)
                    {
                        percentageFraction = (elementValue - elementMinimum) / (elementMaximum - elementMinimum);
                    }
                    else
                    {
                        percentageFraction = 0.0;
                    }
                    if (percentageFraction < 0.0 || percentageFraction > 1.0)
                    {
                        Debug.WriteLine("WARNING: ConsoleProgressBar's .Value is not within the range of .Minimum to .Maximum (or .Minimum >= .Maximum); drawing ProgressBar as 0% progress.");
                        percentageFraction = 0.0;
                    }
                    // NOTE: we may want to consider a "round" option for the chars in the future (at least for all but the final char fill position), as well as a "fill first char at >0%" option...so that the user has a better feeling as to the movement of progress
                    var numberOfFilledChars = (int)(elementWidth * percentageFraction);
                    var numberOfUnfilledChars = (int)(elementWidth - numberOfFilledChars);
                    //
                    //progressBarStringBuilder.Append(DARK_SHADE_CHAR, numberOfFilledChars);
                    progressBarStringBuilder.Append(FULL_BLOCK_CHAR, numberOfFilledChars);
                    progressBarStringBuilder.Append(LIGHT_SHADE_CHAR, numberOfUnfilledChars);
                    var progressBarString = progressBarStringBuilder.ToString();

                    // write out progress bar
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                    //
                    Console.Write(progressBarString);

                    // reset the background/foreground colors for the trailing padding (spaces)
                    if (Console.BackgroundColor != originalBackgroundColor)
                    {
                        Console.BackgroundColor = originalBackgroundColor;
                    }
                    if (Console.ForegroundColor != originalForegroundColor)
                    {
                        Console.ForegroundColor = originalForegroundColor;
                    }
                    //
                    // write out trailing padding (spaces)
                    Console.Write(new String(' ', TRAILING_PADDING_WIDTH));

                    // write out optional trailing text
                    if (elementTrailingText is not null)
                    {
                        if (elementTrailingTextBackgroundColor is not null)
                        {
                            Console.BackgroundColor = elementTrailingTextBackgroundColor.Value;
                        }
                        if (elementTrailingTextForegroundColor is not null)
                        {
                            Console.ForegroundColor = elementTrailingTextForegroundColor.Value;
                        }
                        //
                        Console.Write(elementTrailingText!);
                    }

                    // if the current text (including any 'empty text') was shorter than our previously-rendered text, then clear the remaining line now
                    if (Console.CursorLeft < _lengthOfLineAtLastUpdate)
                    {
                        // reset the background/foreground colors to clear out the remainder of the line
                        if (Console.BackgroundColor != originalBackgroundColor)
                        {
                            Console.BackgroundColor = originalBackgroundColor;
                        }
                        if (Console.ForegroundColor != originalForegroundColor)
                        {
                            Console.ForegroundColor = originalForegroundColor;
                        }

                        var currentCursorLeft = Console.CursorLeft;
                        Console.Write(new String(' ', _lengthOfLineAtLastUpdate - currentCursorLeft));
                        Console.SetCursorPosition(currentCursorLeft, Console.CursorTop);
                    }

                    // update our 'lengthOfLineAtLastUpdate' variable to represent the length which we just printed out
                    _lengthOfLineAtLastUpdate = Console.CursorLeft;
                }
                else
                {
                    // clear the line
                    //
                    // write out empty spaces to clear the line
                    Console.Write(new String(' ', _lengthOfLineAtLastUpdate));
                    //
                    // reset the cursor to the beginning of the current line (again)
                    Console.SetCursorPosition(0, Console.CursorTop);
                    //
                    // reset our "lengthOfLineAtLastUpdate" to zero (since Clear() is effectively an update which cleared out the line completely)
                    _lengthOfLineAtLastUpdate = 0;
                }

                // restore the original background/foreground colors
                Console.BackgroundColor = originalBackgroundColor;
                Console.ForegroundColor = originalForegroundColor;
            }
        }

    }
}
