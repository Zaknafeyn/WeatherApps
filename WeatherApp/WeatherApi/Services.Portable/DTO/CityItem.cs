namespace Services.Portable.DTO
{
    public class CityItem
    {
        public int CityId { get; set; }
        public string CityName { get; set; }

        public override bool Equals(object obj)
        {
            var cityObj = obj as CityItem;

            return CityId == cityObj?.CityId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}