namespace ACCSApi.Model.Interfaces
{
    public interface IIRCode
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        ICode Code { get; }

        bool IsTurnOffCode { get; set; }

        object SettingsArray { get; }
    }
}
