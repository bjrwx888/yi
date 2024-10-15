using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Yi.Framework.DigitalCollectibles.Domain.Shared.Consts;

namespace Yi.Framework.DigitalCollectibles.Domain.Shared.Attributes;

public class ProbabilityAttribute : Attribute
{
    public double Value { get; set; }

    public ProbabilityAttribute(double value)
    {
        this.Value = value;
    }
}

