using ACCSApi.Model.Interfaces;

namespace ACCSApi.Model.Dto
{
    public class AcDeviceDto
    {
        public AcDeviceDto() { }

        public AcDeviceDto(IACDevice acDevice)
        {
            Id = acDevice.Id;
            Model = acDevice.Model;
            Brand = acDevice.Brand;
        }

        public int Id { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
    }
}
