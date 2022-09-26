using System.ComponentModel.DataAnnotations;

namespace EmptyProject.Tools
{
    public class ValidEmailDomainAttribute : ValidationAttribute
    {
        private readonly string domain;

        public ValidEmailDomainAttribute(string Domain)
        {
            this.domain = Domain;
        }


        public override bool IsValid(object value)
        {
            string[] values = value.ToString().Split("@");

            if (values[values.Length - 1].ToLower() == this.domain.ToLower())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
