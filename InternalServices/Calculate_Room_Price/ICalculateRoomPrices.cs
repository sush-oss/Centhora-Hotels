namespace Centhora_Hotels.InternalServices.Calculate_Room_Price
{
    public interface ICalculateRoomPrices
    {
        public decimal CalculateTotalPrice(int NumOfRooms, decimal price);
    }
}
