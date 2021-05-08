namespace SimpleCalculator.Models
{
    class ResultItem
    {
        public double Result { get; set; }
        public string Experssion { get; set; }

        public static bool operator ==(ResultItem left, ResultItem right)
        {
            return left.Result == right.Result && left.Experssion == right.Experssion;
        }

        public static bool operator !=(ResultItem left, ResultItem right)
        {
            return !(left == right);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
