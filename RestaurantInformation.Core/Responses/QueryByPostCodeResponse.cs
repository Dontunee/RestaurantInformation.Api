using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantInformation.Core.Responses
{
    public class QueryByPostCodeResponse
    {
        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("MetaData")]
        public MetaData MetaData { get; set; }

        [JsonProperty("Restaurants")]
        public Restaurant[] Restaurants { get; set; }

        [JsonProperty("RestaurantSets")]
        public object[] RestaurantSets { get; set; }

        [JsonProperty("CuisineSets")]
        public Cuisineset[] CuisineSets { get; set; }

        [JsonProperty("Views")]
        public object[] Views { get; set; }

        [JsonProperty("Dishes")]
        public object[] Dishes { get; set; }

        [JsonProperty("ShortResultText")]
        public string ShortResultText { get; set; }

        [JsonProperty("deliveryFees")]
        public Deliveryfees DeliveryFees { get; set; }

        [JsonProperty("promotedPlacement")]
        public Promotedplacement PromotedPlacement { get; set; }
    }

    public class MetaData
    {


        [JsonProperty("CanonicalName")]
        public string CanonicalName { get; set; }

        [JsonProperty("District")]
        public string District { get; set; }

        [JsonProperty("Postcode")]
        public string Postcode { get; set; }

        [JsonProperty("Area")]
        public string Area { get; set; }

        [JsonProperty("Latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? longitude { get; set; }

        [JsonProperty("CuisineDetails")]
        public Cuisinedetail[] CuisineDetails { get; set; }

        [JsonProperty("ResultCount")]
        public long? ResultCount { get; set; }

        [JsonProperty("SearchedTerms")]
        public object SearchedTerms { get; set; }

        [JsonProperty("TagDetails")]
        public Tagdetail[] TagDetails { get; set; }

    }
    public class Cuisinedetail
    {
        public string Name { get; set; }
        public string SeoName { get; set; }
        public long? Total { get; set; }
    }

    public class Tagdetail
    {
        public string BackgroundColour { get; set; }
        public string Colour { get; set; }
        public string DisplayName { get; set; }
        public string Key { get; set; }
        public long? Priority { get; set; }
    }

    public class Deliveryfees
    {
        public Restaurants restaurants { get; set; }
    }

    public class Restaurants
    {
        public RestaurantInfo RestaurantInfo { get; set; }
    }

    public class RestaurantInfo
    {
        public string restaurantId { get; set; }
        public long? minimumOrderValue { get; set; }
        public Bands bands { get; set; }
    }

    public class Bands
    {
        public long? minimumAmount { get; set; }
        public long? fee { get; set; }
    }

    public class Promotedplacement
    {
        public long? filteredSearchPromotedLimit { get; set; }
        public Restaurants restaurants { get; set; }
        public long?[] rankedIds { get; set; }
    }

    public class Restaurant
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string UniqueName { get; set; }
        public Address Address { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public double? Latitude { get; set; }
        public double? longitude { get; set; }
        public Rating Rating { get; set; }
        public long? RatingStars { get; set; }
        public long? NumberOfRatings { get; set; }
        public long? RatingAverage { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string LogoUrl { get; set; }
        public bool IsTestRestaurant { get; set; }
        public bool IsHalal { get; set; }
        public bool IsNew { get; set; }
        public string ReasonWhyTemporarilyOffline { get; set; }
        public long? DriveDistance { get; set; }
        public bool DriveInfoCalculated { get; set; }
        public bool IsCloseBy { get; set; }
        public long? OfferPercent { get; set; }
        public DateTime? NewnessDate { get; set; }
        public DateTime? OpeningTime { get; set; }
        public object OpeningTimeUtc { get; set; }
        public DateTime? OpeningTimeIso { get; set; }
        public DateTime? OpeningTimeLocal { get; set; }
        public DateTime? DeliveryOpeningTimeLocal { get; set; }
        public DateTime? DeliveryOpeningTime { get; set; }
        public object DeliveryOpeningTimeUtc { get; set; }
        public DateTime? DeliveryStartTime { get; set; }
        public object DeliveryTime { get; set; }
        public object DeliveryTimeMinutes { get; set; }
        public long? DeliveryWorkingTimeMinutes { get; set; }
        public Deliveryetaminutes DeliveryEtaMinutes { get; set; }
        public bool IsCollection { get; set; }
        public bool IsDelivery { get; set; }
        public bool IsFreeDelivery { get; set; }
        public bool IsOpenNowForCollection { get; set; }
        public bool IsOpenNowForDelivery { get; set; }
        public bool IsOpenNowForPreorder { get; set; }
        public bool IsOpenNow { get; set; }
        public bool IsTemporarilyOffline { get; set; }
        public long? DeliveryMenuId { get; set; }
        public object CollectionMenuId { get; set; }
        public object DeliveryZipcode { get; set; }
        public float DeliveryCost { get; set; }
        public long? MinimumDeliveryValue { get; set; }
        public long? SecondDateRanking { get; set; }
        public long? DefaultDisplayRank { get; set; }
        public long? SponsoredPosition { get; set; }
        public long? SecondDateRank { get; set; }
        public long? Score { get; set; }
        public bool IsTemporaryBoost { get; set; }
        public bool IsSponsored { get; set; }
        public bool IsPremier { get; set; }
        public object HygieneRating { get; set; }
        public bool ShowSmiley { get; set; }
        public object SmileyDate { get; set; }
        public bool SmileyElite { get; set; }
        public object SmileyResult { get; set; }
        public object SmileyUrl { get; set; }
        public bool SendsOnItsWayNotifications { get; set; }
        public string BrandName { get; set; }
        public bool IsBrand { get; set; }
        public DateTime? LastUpdated { get; set; }
        public Deal[] Deals { get; set; }
        public Offer[] Offers { get; set; }
        public Logo[] Logo { get; set; }
        public object[] Tags { get; set; }
        public object[] DeliveryChargeBands { get; set; }
        public Cuisinetype[] CuisineTypes { get; set; }
        public Cuisine[] Cuisines { get; set; }
        public Scoremetadata[] ScoreMetaData { get; set; }
        public object[] Badges { get; set; }
        public object[] OpeningTimes { get; set; }
        public object[] ServiceableAreas { get; set; }
    }

    public class Address
    {
        public string City { get; set; }
        public string FirstLine { get; set; }
        public string Postcode { get; set; }
        public float Latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Rating
    {
        public long? Count { get; set; }
        public long? Average { get; set; }
        public long? StarRating { get; set; }
    }

    public class Deliveryetaminutes
    {
        public object Approximate { get; set; }
        public long? RangeLower { get; set; }
        public long? RangeUpper { get; set; }
    }

    public class Deal
    {
        public string Description { get; set; }
        public long? DiscountPercent { get; set; }
        public long? QualifyingPrice { get; set; }
        public string OfferType { get; set; }
    }

    public class Offer
    {
        public long? Amount { get; set; }
        public long? QualifyingValue { get; set; }
        public long? MaxQualifyingValue { get; set; }
        public string Type { get; set; }
        public string OfferId { get; set; }
    }

    public class Logo
    {
        public string StandardResolutionURL { get; set; }
    }

    public class Cuisinetype
    {
        public long? Id { get; set; }
        public bool IsTopCuisine { get; set; }
        public string Name { get; set; }
        public string SeoName { get; set; }
    }

    public class Cuisine
    {
        public string Name { get; set; }
        public string SeoName { get; set; }
    }

    public class Scoremetadata
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public class Cuisineset
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Cuisine[] Cuisines { get; set; }
    }











}





