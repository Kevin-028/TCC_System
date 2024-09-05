using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TCC_System_Domain.Core
{
    public static class AssertionConcern
    {
        public static bool IsSatisfiedBy(params DomainNotification[] validations)
        {
            var notificationsNotNull = validations.Where(validation => validation != null);
            NotifyAll(notificationsNotNull);

            return notificationsNotNull.Count().Equals(0);
        }

        private static void NotifyAll(IEnumerable<DomainNotification> notifications)
        {
            notifications.ToList().ForEach(DomainEvent.Raise);
        }

        public static void AssertNotification(string message)
        {
            IsSatisfiedBy(new DomainNotification("AssertException", message));
        }

        public static DomainNotification AssertLength(string stringValue, int minimum, int maximum, string message)
        {
            int length = stringValue.Trim().Length;

            return length < minimum || length > maximum ?
                new DomainNotification("AssertArgumentLength", message) : null;
        }

        public static DomainNotification AssertFixedLength(string stringValue, int size, string message)
        {
            int length = stringValue.Trim().Length;

            return length != size ?
                new DomainNotification("AssertArgumentFixedLength", message) : null;
        }

        public static DomainNotification AssertMatches(string pattern, string stringValue, string message)
        {
            Regex regex = new Regex(pattern);

            return !regex.IsMatch(stringValue) ?
                new DomainNotification("AssertArgumentLength", message) : null;
        }

        public static DomainNotification AssertNotNullOrEmpty(string stringValue, string message)
        {
            return string.IsNullOrEmpty(stringValue) ?
                new DomainNotification("AssertArgumentNotEmpty", message) : null;
        }

        public static DomainNotification AssertNotNullOrEmpty(List<string> stringValue, string message)
        {
            return stringValue.Count == 0 ?
                new DomainNotification("AssertArgumentNotEmpty", message) : null;
        }

        public static DomainNotification AssertNotNull(object object1, string message)
        {
            return object1 == null ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }

        public static DomainNotification AssertQuantityMoreThanZero(int value, string message)
        {
            return value <= 0 ?
                new DomainNotification("AssertArgumentQuantityIqualsZero", message) : null;
        }

        public static DomainNotification AssertQuantityMoreThanZero(double value, string message)
        {
            return value <= 0 ?
                new DomainNotification("AssertArgumentQuantityIqualsZero", message) : null;
        }

        public static DomainNotification AssertQuantityMoreThanZero(decimal value, string message)
        {
            return value <= 0 ?
                new DomainNotification("AssertArgumentQuantityIqualsZero", message) : null;
        }

        public static DomainNotification AssertQuantityMoreThanZero(long value, string message)
        {
            return value <= 0 ?
                new DomainNotification("AssertArgumentQuantityIqualsZero", message) : null;
        }

        public static DomainNotification AssertQuantityEqualsZero(int value, string message)
        {
            return value == 0 ? null :
                new DomainNotification("AssertArgumentQuantityIqualsZero", message);
        }

        public static DomainNotification AssertQuantityEqualsZero(double value, string message)
        {
            return value == 0 ? null :
                new DomainNotification("AssertArgumentQuantityIqualsZero", message);
        }

        public static DomainNotification AssertQuantityEqualsZero(decimal value, string message)
        {
            return value == 0 ? null :
                new DomainNotification("AssertArgumentQuantityIqualsZero", message);
        }

        public static DomainNotification AssertQuantityEqualsZero(long value, string message)
        {
            return value == 0 ? null :
                new DomainNotification("AssertArgumentQuantityIqualsZero", message);
        }

        public static DomainNotification AssertTrue(bool boolValue, string message)
        {
            return !boolValue ?
                new DomainNotification("AssertArgumentTrue", message) : null;
        }

        public static DomainNotification AssertAreEquals(string value, string match, string message)
        {
            return !(value == match) ?
                new DomainNotification("AssertArgumentTrue", message) : null;
        }

        public static DomainNotification AssertAreNotEquals(string value, string match, string message)
        {
            return value == match ?
                new DomainNotification("AssertArgumentTrue", message) : null;
        }

        public static DomainNotification AssertNotNullAndBiggerThanZero(int? intValue, string message)
        {
            return intValue == null || intValue.Value <= 0 ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }

        public static DomainNotification AssertDatetimeNotNull(DateTime? dateValue, string message)
        {
            return dateValue == null ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }

        public static DomainNotification AssertDatetimeNotNull(DateTime dateValue, string message)
        {
            return dateValue == DateTime.MinValue ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }

        public static DomainNotification AssertDatetimeNotEquals(DateTime? dateValueBegin, DateTime? dateValueEnd, string message)
        {
            return dateValueBegin == dateValueEnd ?
                new DomainNotification("AssertArgumentTrue", message) : null;
        }

        public static DomainNotification AssertEmailIsValid(string email, string message)
        {
            var regexEmail = new Regex(@"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");

            return !regexEmail.IsMatch(email) ?
                new DomainNotification("AssertArgumentNotNull", message) : null;
        }
    }
}