namespace SmartCharging.API.Domain.Utilities
{
    public static class RangeValues
    {
        public static int GetMinumumCapacityInAmps()
        {
            return 0;
        }

        public static int GetMinumumNumberOfConnectors()
        {
            return 1;
        }

        public static int GetMaximumNumberOfConnectors()
        {
            return 5;
        }
    }
}
