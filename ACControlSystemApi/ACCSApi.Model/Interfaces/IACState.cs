namespace ACCSApi.Model.Interfaces
{
    public interface IACState
    {
        bool? IsTurnOff { get; set; }
        IACSetting ACSetting { get; set; } //todo: think about changing it to setting id?
    }
}
