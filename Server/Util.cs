namespace Server
{
    public class Util
    {
        public static bool IsNumber(string s)
        {
            if (s == null)
                return false;

            s = s.Trim();
            int len = s.Length;
            if (len == 0)
                return false;
            else if (len == 1)
            {
                if (s[0] == '.' || s[0] == '+' || s[0] == '-')
                    return false;
            }

            if (s[0] == 'e' || s[0] == 'E' ||
                    s[len - 1] == 'e' || s[len - 1] == 'E')
                return false;

            // 小数
            bool isDecimal = false;
            // 指数
            bool isExponent = false;
            // 负数
            bool isNegative = false;
            // 正数
            bool isPositive = false;
            for (int i = 0; i < len; i++)
            {
                if (!isDecimal && s[i] == '.')
                    isDecimal = true;
                else if (!isExponent && (s[i] == 'e' || s[i] == 'E'))
                    isExponent = true;
                else if (!isNegative && s[i] == '-')
                    isNegative = true;
                else if (!isPositive && s[i] == '+')
                    isPositive = true;
                else if (s[i] < '0' || s[i] > '9')
                    return false;
            }
            return true;
        }
    }
}
