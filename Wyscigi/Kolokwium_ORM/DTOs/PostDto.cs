using Kolokwium_ORM.Models;

namespace Kolokwium_ORM.DTOs;

public class PostDto
{
    public string raceName { get; set; }
    public string trackName { get; set; }
    public IEnumerable<InesrtRaceParticipationDTO> participations { get; set; }



    public class InesrtRaceParticipationDTO
    {
        public int racerId { get; set; }
        public int position { get; set; }
        public int finishTimeInSeconds { get; set; }
    }
}