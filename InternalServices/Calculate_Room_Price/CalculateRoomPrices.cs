namespace Centhora_Hotels.InternalServices.Calculate_Room_Price
{
    public class CalculateRoomPrices : ICalculateRoomPrices
    {
        public decimal CalculateTotalPrice(int NumOfRooms, decimal price)
        {
            decimal fixedRoomServiceFee = 60;
            decimal totoalPrice = 0;
            decimal Price = price;
            int NumOfRoomsBooked = NumOfRooms;


            if (NumOfRoomsBooked > 0)
            {
                totoalPrice = NumOfRoomsBooked * Price + fixedRoomServiceFee;
            }
            return totoalPrice;
        }
    }
}
