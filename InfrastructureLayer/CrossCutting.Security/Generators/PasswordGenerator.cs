using System;
using System.Linq;

namespace CrossCutting.Security.Generators
{
    /// <summary>
    /// Random password generator
    /// </summary>
    public class PasswordGenerator
    {
        public PasswordOptions PwdOptions { get; }

        public string SpecialChars => _allSpecialChars;

        private static readonly string _allLowerCaseChars;
        private static readonly string _allUpperCaseChars;
        private static readonly string _allNumericChars;
        private static readonly string _allSpecialChars;

        private readonly SecureRNG _secureRng = new SecureRNG();
        private readonly string _availableChars;
        private int _minimumNumberOfChars;
 

        static PasswordGenerator()
        {
            // Set available char ranges
            _allLowerCaseChars = GetCharRange('a', 'z'); 
            _allUpperCaseChars = GetCharRange('A', 'Z');
            _allNumericChars = GetCharRange('0', '9');
            _allSpecialChars = "!@#$?_";
        }

        public PasswordGenerator(PasswordOptions options)
        {
            if (options.MinimumLength < 1)
            {
                throw new ArgumentException("The minimum length is smaller than 1.", nameof(options.MinimumLength));
            }
 
            if (options.MinimumLength > options.MaximumLength)
            {
                throw new ArgumentException("The minimum length is bigger than the maximum length.", nameof(options.MaximumLength));
            }

            _minimumNumberOfChars = options.MinimumLowerCaseChars + options.MinimumUpperCaseChars + options.MinimumNumericChars + options.MinimumSpecialChars;
 
            if (options.MinimumLength < _minimumNumberOfChars)
            {
                throw new ArgumentException("The minimum length ot the password is smaller than the sum of the minimum characters of all categories.", nameof(options.MinimumLength));
            }

            PwdOptions = options;
 
            _availableChars =
                GetCharsIfRequired(options.MinimumLowerCaseChars, _allLowerCaseChars) +
                GetCharsIfRequired(options.MinimumUpperCaseChars, _allUpperCaseChars) +
                GetCharsIfRequired(options.MinimumNumericChars, _allNumericChars) +
                GetCharsIfRequired(options.MinimumSpecialChars, _allSpecialChars);
        }
 
        private string GetCharsIfRequired(int minimum, string allChars)
        {
            return minimum > 0 || _minimumNumberOfChars == 0 ? allChars : string.Empty;
        }
 
        public string Generate()
        {
            int pwdLength = _secureRng.Next(PwdOptions.MinimumLength, PwdOptions.MaximumLength);
             
            // Get the required number of characters of each catagory and add random charactes of all catagories
            string minimumChars = 
                GetRandomString(_allLowerCaseChars, PwdOptions.MinimumLowerCaseChars) +
                GetRandomString(_allUpperCaseChars, PwdOptions.MinimumUpperCaseChars) + 
                GetRandomString(_allNumericChars, PwdOptions.MinimumNumericChars) +
                GetRandomString(_allSpecialChars, PwdOptions.MinimumSpecialChars);

            string rest = GetRandomString(_availableChars, pwdLength - minimumChars.Length);

            string unshuffledResult = minimumChars + rest;
             
            // Shuffle the result so the order of the characters are unpredictable
            string result = unshuffledResult.SecureTextShuffle();

            return result;
        }
 
        private string GetRandomString(string possibleChars, int lenght)
        {
            string result = string.Empty;

            for (int position = 0; position < lenght; position++)
            {
                int index = _secureRng.Next(possibleChars.Length);
                result += possibleChars[index];
            }

            return result;
        }
 
        private static string GetCharRange(char minimum, char maximum, string exclusiveChars = "")
        {
            string result = string.Empty;

            for (char value = minimum; value <= maximum; value++)
            {
                result += value;
            }

            if (!string.IsNullOrEmpty(exclusiveChars))
            {
                char[] inclusiveChars = result.Except(exclusiveChars).ToArray();
                result = new string(inclusiveChars);
            }

            return result;
        }

        public class PasswordOptions
        {
            private byte _minimumLength;

            public byte MinimumLength
            {
                get => _minimumLength;
                set
                {
                    _minimumLength = value;

                    if (value > MaximumLength)
                    {
                        MaximumLength = value;
                    }
                }
            }

            private byte _maximumLength;

            public byte MaximumLength
            {
                get => _maximumLength;
                set
                {
                    _maximumLength = value;

                    if (value < MinimumLength)
                    {
                        MinimumLength = value;
                    }
                }
            }

            public byte MinimumLowerCaseChars { get; set; }

            public byte MinimumUpperCaseChars { get; set; }

            public byte MinimumNumericChars { get; set; }

            public byte MinimumSpecialChars { get; set; }

            public PasswordOptions()
            {
                MinimumLength = 8;
                MaximumLength = MinimumLength;
                
                MinimumLowerCaseChars = 0;
                MinimumUpperCaseChars = 0;
                MinimumNumericChars = 0;
                MinimumSpecialChars = 0;
            }

            public PasswordOptions(byte length)
            {
                MinimumLength = length;
                MaximumLength = length;
                
                MinimumLowerCaseChars = 0;
                MinimumUpperCaseChars = 0;
                MinimumNumericChars = 0;
                MinimumSpecialChars = 0;
            }
        }
    }
}
