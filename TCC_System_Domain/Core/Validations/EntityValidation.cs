using System;

namespace TCC_System_Domain.Core
{
    public static class EntityValidation
    {
        public static string SetStringProperty(string value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value.Trim();

            return null;
        }

        public static string SetStringTitleCaseProperty(string value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return TextHelper.ToTitleCase(value.Trim());

            return null;
        }

        public static string SetStringUpperCaseProperty(string value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value.Trim().ToUpper();

            return null;
        }

        public static string SetEmailProperty(string email)
        {
            if (EmailIsValid(email))
            {
                return email;
            }

            return null;
        }

        public static int SetIntProperty(int value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return 0;
        }

        public static int? SetNullIntProperty(int? value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return null;
        }

        public static double SetDoubleProperty(double value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return 0;
        }

        public static decimal SetDecimalProperty(decimal value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return 0;
        }

        public static int? SetIntProprety(int vercao, string v)
        {
            throw new NotImplementedException();
        }

        public static long SetLongProperty(long value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return 0;
        }

        public static Guid SetGuidProperty(Guid value, string propertyName)
        {
            if (value.Equals(Guid.Empty))
            {
                AssertionConcern.AssertNotification(propertyName);
                return Guid.Empty;
            }

            return value;
        }

        public static object SetObjectProperty(object value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return null;
        }

        public static DateTime SetDateTimeProperty(DateTime value, string propertyName)
        {
            if (VerifyPropertyIsValid(value, propertyName))
                return value;

            return DateTime.MinValue;
        }

        public static string SetPropertyVerifyingValueIsAnInteger(string value, string propertyName)
        {
            if (!int.TryParse(value, out int valueConversion))
            {
                AssertionConcern.AssertNotification(propertyName);
                return string.Empty;
            }

            return value;
        }

        public static bool VerifyPropertyIsValid(string value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNullOrEmpty(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(int? value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNullAndBiggerThanZero(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(int value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertQuantityMoreThanZero(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(double value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertQuantityMoreThanZero(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(decimal value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertQuantityMoreThanZero(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(long value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertQuantityMoreThanZero(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(object value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertNotNull(value, propertyName)
            );
        }

        public static bool VerifyPropertyIsValid(DateTime value, string propertyName)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertDatetimeNotNull(value, propertyName)
            );
        }

        public static bool EmailIsValid(string email)
        {
            return AssertionConcern.IsSatisfiedBy
            (
                AssertionConcern.AssertEmailIsValid(email, "The Email is incorrect."),
                AssertionConcern.AssertNotNullOrEmpty(email, "The Email needs to be filled properly.")
            );
        }
    }
}
