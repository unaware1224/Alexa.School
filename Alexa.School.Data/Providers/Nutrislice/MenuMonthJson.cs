using System;
using System.Collections.Generic;
using System.Text;

namespace Alexa.School.Data.Menus.Food.Provider.Nutrislice
{
    /// <summary>
    /// Represents the JSON data on the nutrislice site.
    /// </summary>
    public class MenuMonthJson
    {
        public List<Day> days { get; set; }
        public bool published { get; set; }
        public int menu_type_id { get; set; }
        public int? id { get; set; }
        public DateTime? last_updated { get; set; }
        public string start_date { get; set; }
    }

    public class Day
    {
        public string date { get; set; }
        public List<Menu_Items> menu_items { get; set; }
    }

    public class Menu_Items
    {
        public int? id { get; set; }
        public string date { get; set; }
        public int? position { get; set; }
        public bool is_section_title { get; set; }
        public bool bold { get; set; }
        public string text { get; set; }
        public bool no_line_break { get; set; }
        public bool blank_line { get; set; }
        public int? menu_type_id { get; set; }
        public Food food { get; set; }
        public int? menu_week_id { get; set; }
        public bool is_holiday { get; set; }
        //public object station_id { get; set; }
        public string category { get; set; }
    }

    public class Food
    {
        public int? id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string subtext { get; set; }
        public string image_url { get; set; }
        public string hoverpic_url { get; set; }
        public string price { get; set; }
        public string ingredients { get; set; }
        public string food_category { get; set; }
        public Food_Highlight_Message food_highlight_message { get; set; }
        public string file_url { get; set; }
        public string download_label { get; set; }
        public Rounded_Nutrition_Info rounded_nutrition_info { get; set; }
        //public Allergy_Info allergy_info { get; set; }
        public Food_Group_Info food_group_info { get; set; }
        public Food_Icon_Info food_icon_info { get; set; }
        public Serving_Size_Info serving_size_info { get; set; }
        public bool has_nutrition_info { get; set; }
    }

    public class Food_Highlight_Message
    {
        public string header_text { get; set; }
        public string normal_text { get; set; }
        public string color { get; set; }
    }

    public class Rounded_Nutrition_Info
    {
        public float? calories { get; set; }
        public float? g_fat { get; set; }
        public float? g_saturated_fat { get; set; }
        public int? g_trans_fat { get; set; }
        public float? mg_cholesterol { get; set; }
        public float? g_carbs { get; set; }
        public float? g_sugar { get; set; }
        public float? mg_sodium { get; set; }
        public float? g_fiber { get; set; }
        public float? g_protein { get; set; }
        public float? mg_iron { get; set; }
        public float? mg_calcium { get; set; }
        public float? mg_vitamin_c { get; set; }
        public float? iu_vitamin_a { get; set; }
        public float? re_vitamin_a { get; set; }
    }

    ////public class Allergy_Info
    ////{
    ////    public Allergen[] allergens { get; set; }
    ////    public string[][] allergies_and_slugs { get; set; }
    ////    public bool complete_and_approved { get; set; }
    ////}

    public class Allergen
    {
        public string name { get; set; }
        public string slug { get; set; }
        public bool use_nutrislice_icon { get; set; }
        public string descriptive_text { get; set; }
        public bool hide_name { get; set; }
        public string type { get; set; }
        public string icon_sprite_css_class { get; set; }
    }

    public class Food_Group_Info
    {
        //public List<string> food_groups { get; set; }
    }

    public class Food_Icon_Info
    {
        //public List<string> food_icons { get; set; }
    }

    public class Serving_Size_Info
    {
        public string serving_size_amount { get; set; }
        public string serving_size_unit { get; set; }
    }
}
