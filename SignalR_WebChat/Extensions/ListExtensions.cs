using SignalR_WebChat.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalR_WebChat.Extensions
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            for (var i = 0; i < list.Count; i++)
            {
                var temp = list[i];
                var index = random.Next(0, list.Count);
                list[i] = list[index];
                list[index] = temp;
            }
        }
        public static bool HasAce<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsAce()))
                return true;
            else
                return false;
        }
        public static bool HasKing<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsKing()))
                return true;
            else
                return false;
        }
        public static bool HasQueen<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsQueen()))
                return true;
            else
                return false;
        }
        public static bool HasJack<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsJack()))
                return true;
            else
                return false;
        }
        public static bool HasTen<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsTen()))
                return true;
            else
                return false;
        }
        public static bool HasClub<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsClubs()))
                return true;
            else
                return false;
        }
        public static bool HasDiamond<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsDiamonds()))
                return true;
            else
                return false;
        }
        public static bool HasHeart<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsHearts()))
                return true;
            else
                return false;
        }
        public static bool HasSpade<T>(this IList<Card> list)
        {
            if (list.Any(c => c.IsSpades()))
                return true;
            else
                return false;
        }
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int takeCount)
        {
            if (source == null) { throw new ArgumentNullException("source"); }
            if (takeCount < 0) { throw new ArgumentOutOfRangeException("takeCount", "must not be negative"); }
            if (takeCount == 0) { yield break; }

            T[] result = new T[takeCount];
            int i = 0;

            int sourceCount = 0;
            foreach (T element in source)
            {
                result[i] = element;
                i = (i + 1) % takeCount;
                sourceCount++;
            }

            if (sourceCount < takeCount)
            {
                takeCount = sourceCount;
                i = 0;
            }

            for (int j = 0; j < takeCount; ++j)
            {
                yield return result[(i + j) % takeCount];
            }
        }

    }
}