using System.Text.Json;

namespace SwEngHomework.Commissions
{
    public class CommissionCalculator : ICommissionCalculator
    {
        public IDictionary<string, double> CalculateCommissionsByAdvisor(string jsonInput)
        {
            Dictionary<string, double> result = new Dictionary<string, double>();
            using (JsonDocument document = JsonDocument.Parse(jsonInput))
            {
                if (document != null)
                {
                    JsonElement root = document.RootElement;

                    var advisors = root.GetProperty("advisors").EnumerateArray();

                    var accounts = root.GetProperty("accounts");
                    
                    foreach (JsonElement advisor in advisors)
                    {
                        string name = advisor.GetProperty("name").GetString();
                        string level = advisor.GetProperty("level").GetString();
                       
                        double commission = CalculateCommission(accounts, name, level);
                        result.Add(name, Math.Round(commission, 2));
                    }
                }
            }
            return result;
        }

        static double CalculateCommission(JsonElement accounts, string advisorName, string advisorLevel)
        {
            var advisorAccounts = accounts
                .EnumerateArray()
                .Where(a => a.GetProperty("advisor").GetString() == advisorName);

            double totalAccountFee = advisorAccounts.Sum(a => CalculateAccountFee(a.GetProperty("presentValue").GetInt32()));

            double commissionRate = GetCommissionRate(advisorLevel);

            double commission = totalAccountFee * commissionRate;

            return commission;
        }

        static double CalculateAccountFee(int presentValue)
        {
            double basisPoints;

            if (presentValue < 50000)
            {
                basisPoints = 5;
            }
            else if (presentValue < 100000)
            {
                basisPoints = 6;
            }
            else
            {
                basisPoints = 7;
            }

            return presentValue * basisPoints / 10000;
        }

        static double GetCommissionRate(string advisorLevel)
        {
            switch (advisorLevel)
            {
                case "Senior":
                    return 1.0;
                case "Experienced":
                    return 0.5;
                case "Junior":
                    return 0.25;
                default:
                    return 0.0;
            }
        }
    }
}
