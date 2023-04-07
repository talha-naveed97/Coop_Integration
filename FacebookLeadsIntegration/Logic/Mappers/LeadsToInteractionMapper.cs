using Models;

namespace Logic.Mappers
{
    public class LeadsToInteractionMapper : IMapper<LeadsAdModel, InteractionModel>
    {
        public InteractionModel Map(LeadsAdModel source)
        {
            // Map leads ad response to interaction
            var interaction = new InteractionModel();
            interaction.CommunicationMedium = source.FieldData.First(fd => fd.FieldName == "CoummunicationMedium").Value; // or simply source.CommunicationMedium;
            // and so on

            return interaction;
        }
    }
}
