using System.Text;

namespace gameteq_task_test.Helpers
{
    public class RandomString
    {
        private string result;
        private int length;
        public RandomString(int length = 10)
        {
            this.length = length;
        }

        public string Generate()
        {
            StringBuilder sb = new();
            Random random = new();

            char ch;

            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(25);
                ch = Convert.ToChar(rnd + 97);
                sb.Append(ch);
            }

            return sb.ToString();
        }
    }
}