﻿using System;
using System.Collections.Generic;

namespace Backend.DTO.NASAResponse
{
    public class NasaResponseDTO
    {
        public PageLinks links { get; set; }
        public int element_count { get; set; }
        public Dictionary<string, List<Date>> near_earth_objects { get; set; }
    }

    public class PageLinks
    {
        public string? next { get; set; }
        public string? prev { get; set; }
        public string self { get; set; }
    }

    public class Date
    {
        public PageLinks links { get; set; }
        public string id { get; set; }
        public string neo_reference_id { get; set; }
        public string name { get; set; }
        public string nasa_jpl_url { get; set; }
        public float absolute_magnitude_h { get; set; }
        public EstimatedDiameter estimated_diameter { get; set; }
        public bool is_potentially_hazardous_asteroid { get; set; }
        public CloseApproachData[] close_approach_data { get; set; }
        public bool is_sentry_object { get; set; }
    }

    public class EstimatedDiameter
    {
        public Kilometers kilometers { get; set; }
        public Meters meters { get; set; }
        public Miles miles { get; set; }
        public Feet feet { get; set; }
    }

    public class CloseApproachData
    {
        public DateTime close_approach_date { get; set; }
        public DateTime close_approach_date_full { get; set; }
        public long epoch_date_close_approach { get; set; }
        public RelativeVelocity relative_velocity { get; set; }
        public MissDistance miss_distance { get; set; }
        public string orbiting_body { get; set; }
    }

    public class Kilometers
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Meters
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Miles
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class Feet
    {
        public float estimated_diameter_min { get; set; }
        public float estimated_diameter_max { get; set; }
    }

    public class RelativeVelocity
    {
        public float kilometers_per_second { get; set; }
        public float kilometers_per_hour { get; set; }
        public float miles_per_hour { get; set; }
    }

    public class MissDistance
    {
        public float astronomical { get; set; }
        public float lunar { get; set; }
        public float kilometers { get; set; }
        public float miles { get; set; }
    }
}