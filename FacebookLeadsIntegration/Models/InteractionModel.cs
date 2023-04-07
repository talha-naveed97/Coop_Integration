namespace Models
{
    public class InteractionModel
    {
        public string InteractionContactOrigin { get; set; }
        public string InteractionContactId { get; set; }
        public string CommunicationMedium { get; set; }
        public string InteractionType { get; set; }
        public string InteractionTimestampUTC { get; set; }
        public string SourceSystemType { get; set; }
        public string SourceSystem { get; set;}
        public string InteractionSourceObjectType { get; set; }
        public string InteractionSourceObject { get; set; }
        public string InteractionSourceObjectAdDId { get; set; }
        public InteractionOfferModel InteractionOffers { get; set; }
    }
}
