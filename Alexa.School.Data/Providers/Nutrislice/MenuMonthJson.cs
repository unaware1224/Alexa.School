using System.Collections.Generic;
using Newtonsoft.Json;

namespace Alexa.School.Data.Providers.Nutrislice
{
    /// <summary>
    ///     Represents the JSON data on the nutrislice site.
    /// </summary>
    public class MenuMonthJson
    {
        #region Properties, Indexers

        [JsonProperty(propertyName: "days")]
        public List<Day> Days { get; set; }

        #endregion

        ////[JsonProperty(propertyName: "published")]
        ////public bool? Published { get; set; }

        ////[JsonProperty(propertyName: "menu_type_id")]
        ////public int? MenuTypeId { get; set; }

        ////[JsonProperty(propertyName: "id")]
        ////public int? Id { get; set; }

        ////[JsonProperty(propertyName: "last_updated")]
        ////public DateTime? LastUpdated { get; set; }

        ////[JsonProperty(propertyName: "start_date")]
        ////public string StartDate { get; set; }
    }

    public class Day
    {
        #region Properties, Indexers

        [JsonProperty(propertyName: "date")]
        public string Date { get; set; }

        [JsonProperty(propertyName: "menu_items")]
        public List<MenuItem> MenuItems { get; set; }

        #endregion
    }

    public class MenuItem
    {
        #region Properties, Indexers

        [JsonProperty(propertyName: "id")]
        public int? Id { get; set; }

        [JsonProperty(propertyName: "date")]
        public string Date { get; set; }

        [JsonProperty(propertyName: "position")]
        public int? Position { get; set; }

        [JsonProperty(propertyName: "is_section_title")]
        public bool IsSectionTitle { get; set; }

        [JsonProperty(propertyName: "bold")]
        public bool Bold { get; set; }

        [JsonProperty(propertyName: "text")]
        public string Text { get; set; }

        [JsonProperty(propertyName: "no_line_break")]
        public bool NoLineBreak { get; set; }

        [JsonProperty(propertyName: "blank_line")]
        public bool? BlankLine { get; set; }

        [JsonProperty(propertyName: "menu_type_id")]
        public int? MenuTypeId { get; set; }

        [JsonProperty(propertyName: "food")]
        public Food Food { get; set; }

        ////[JsonProperty(propertyName: "menu_week_id")]
        ////public int? MenuWeekId { get; set; }

        ////[JsonProperty(propertyName: "is_holiday")]
        ////public bool IsHoliday { get; set; }

        ////public object station_id { get; set; }

        [JsonProperty(propertyName: "category")]
        public string Category { get; set; }

        #endregion
    }

    public class Food
    {
        #region Properties, Indexers

        [JsonProperty(propertyName: "id")]
        public int? Id { get; set; }

        [JsonProperty(propertyName: "name")]
        public string Name { get; set; }

        ////[JsonProperty(propertyName: "description")]
        ////public string Description { get; set; }

        ////[JsonProperty(propertyName: "subtext")]
        ////public string Subtext { get; set; }

        ////[JsonProperty(propertyName: "image_url")]
        ////public string ImageUrl { get; set; }

        ////[JsonProperty(propertyName: "hoverpic_url")]
        ////public string HoverpicUrl { get; set; }

        ////[JsonProperty(propertyName: "price")]
        ////public string Price { get; set; }

        ////[JsonProperty(propertyName: "ingredients")]
        ////public string Ingredients { get; set; }

        [JsonProperty(propertyName: "food_category")]
        public string FoodCategory { get; set; }

        #endregion

        ////[JsonProperty(propertyName: "food_highlight_message")]
        ////public FoodHighlightMessage FoodHighlightMessage { get; set; }

        ////[JsonProperty(propertyName: "file_url")]
        ////public string FileUrl { get; set; }

        ////[JsonProperty(propertyName: "download_label")]
        ////public string DownloadLabel { get; set; }

        ////[JsonProperty(propertyName: "rounded_nutrition_info")]
        ////public RoundedNutritionInfo RoundedNutritionInfo { get; set; }

        ////////public Allergy_Info allergy_info { get; set; }

        ////[JsonProperty(propertyName: "food_group_info")]
        ////public FoodGroupInfo FoodGroupInfo { get; set; }

        ////[JsonProperty(propertyName: "food_icon_info")]
        ////public FoodIconInfo FoodIconInfo { get; set; }

        ////[JsonProperty(propertyName: "serving_size_info")]
        ////public ServingSizeInfo ServingSizeInfo { get; set; }

        ////[JsonProperty(propertyName: "has_nutrition_info")]
        ////public bool? HasNutritionInfo { get; set; }
    }

    ////public class FoodHighlightMessage
    ////{
    ////    #region Properties, Indexers

    ////    [JsonProperty(propertyName: "header_text")]
    ////    public string HeaderText { get; set; }

    ////    [JsonProperty(propertyName: "normal_text")]
    ////    public string NormalText { get; set; }

    ////    [JsonProperty(propertyName: "color")]
    ////    public string Color { get; set; }

    ////    #endregion
    ////}

    ////public class RoundedNutritionInfo
    ////{
    ////    #region Properties, Indexers

    ////    [JsonProperty(propertyName: "calories")]
    ////    public float? Calories { get; set; }

    ////    [JsonProperty(propertyName: "g_fat")]
    ////    public float? GFat { get; set; }

    ////    [JsonProperty(propertyName: "g_saturated_fat")]
    ////    public float? GSaturatedFat { get; set; }

    ////    [JsonProperty(propertyName: "g_trans_fat")]
    ////    public int? GTransFat { get; set; }

    ////    [JsonProperty(propertyName: "mg_cholesterol")]
    ////    public float? MgCholesterol { get; set; }

    ////    [JsonProperty(propertyName: "g_carbs")]
    ////    public float? GCarbs { get; set; }

    ////    [JsonProperty(propertyName: "g_sugar")]
    ////    public float? GSugar { get; set; }

    ////    [JsonProperty(propertyName: "mg_sodium")]
    ////    public float? MgSodium { get; set; }

    ////    [JsonProperty(propertyName: "g_fiber")]
    ////    public float? GFiber { get; set; }

    ////    [JsonProperty(propertyName: "g_protein")]
    ////    public float? GProtein { get; set; }

    ////    [JsonProperty(propertyName: "mg_iron")]
    ////    public float? MgIron { get; set; }

    ////    [JsonProperty(propertyName: "mg_calcium")]
    ////    public float? MgCalcium { get; set; }

    ////    [JsonProperty(propertyName: "mg_vitamin_c")]
    ////    public float? MgVitaminC { get; set; }

    ////    [JsonProperty(propertyName: "iu_vitamin_a")]
    ////    public float? IuVitaminA { get; set; }

    ////    [JsonProperty(propertyName: "re_vitamin_a")]
    ////    public float? ReVitaminA { get; set; }

    ////    #endregion
    ////}

    ////public class Allergy_Info
    ////{
    ////    public Allergen[] allergens { get; set; }
    ////    public string[][] allergies_and_slugs { get; set; }
    ////    public bool complete_and_approved { get; set; }
    ////}

    ////public class Allergen
    ////{
    ////    #region Properties, Indexers

    ////    [JsonProperty(propertyName: "name")]
    ////    public string Name { get; set; }

    ////    [JsonProperty(propertyName: "slug")]
    ////    public string Slug { get; set; }

    ////    [JsonProperty(propertyName: "use_nutrislice_icon")]
    ////    public bool UseNutrisliceIcon { get; set; }

    ////    [JsonProperty(propertyName: "descriptive_text")]
    ////    public string DescriptiveText { get; set; }

    ////    [JsonProperty(propertyName: "hide_name")]
    ////    public bool HideName { get; set; }

    ////    [JsonProperty(propertyName: "type")]
    ////    public string Type { get; set; }

    ////    [JsonProperty(propertyName: "icon_sprite_css_class")]
    ////    public string IconSpriteCssClass { get; set; }

    ////    #endregion
    ////}

    ////public class FoodGroupInfo
    ////{
    ////    //public List<string> food_groups { get; set; }
    ////}

    ////public class FoodIconInfo
    ////{
    ////    //public List<string> food_icons { get; set; }
    ////}

    ////public class ServingSizeInfo
    ////{
    ////    #region Properties, Indexers

    ////    [JsonProperty(propertyName: "serving_size_amount")]
    ////    public string ServingSizeAmount { get; set; }

    ////    [JsonProperty(propertyName: "serving_size_unit")]
    ////    public string ServingSizeUnit { get; set; }

    ////    #endregion
    ////}
}