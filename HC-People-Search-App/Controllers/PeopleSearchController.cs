using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HC_People_Search_App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PeopleSearchController : ControllerBase
    {
        private static readonly string[] fName = new[]
        {
            "Jim", "Sarah", "Alex", "Amy", "Tim", "Lex", "Amos", "Lee", "Taylor", "Reagan", "Cameron", "James", "Zack", "Zach", "Alex", "Harmony", "Eliot", "Ellen", "Aphrodite", "Cleo", "Bill", "Ted", "Marcus", "Noah", "Weston", "Ben", "Katie", "Harlly", "Marly", "Susanna", "Maria", "Stanley", "Han", "Luke", "Landon", "Thor"
        };
        private static readonly string[] lName = new[]
        {
            "Smith", "Moa", "McCormick", "Caballero", "Faulkner", "Kamiesoko", "Leland", "Gosset", "Holt", "Hudgins", "Evans", "Brown", "Rawlins", "Oneill", "Oneil", "Carter", "Jackson", "Willson", "Hammond", "Ragnarson", "Erickson", "Washington", "Harper", "Fallow", "McCloud", "McCleod", "Lee", "Leeland", "Carlson", "Annson", "Clark", "Shalows", "Winchester", "Adams", "Schumester", "Kelly", "Yelnats", "Solo", "Skywalker", "Thorson", "Willcaster"
        };
        private static readonly string[] interests = new[]
        {
            "Golf", "Reading", "Gaming", "Disc Golf", "Rock Climbing", "Tennis", "Swimming", "Skiing", "Snowboarding", "Guitar", "Photography", "Gardening", "Cycling", "Mountain Biking", "Bouldering", "Working Out", "TV", "Movies", "Filmography", "Sleep", "Food", "Cooking", "Cats", "Dogs", "Art", "Painting", "Woodworking", "Drawing", "Digital Animation", "Digital Artwork", "Buying $GME", "Holding GME", "Social Media", "Driving", "Racing", "Motorcycles", "Dirt Bikes", "ATV's", "Horse Riding", "Grilling", "Cars", "Music", "Hiking", "Kite Surfing", "Surfing", "Kayaking", "Boating", "Fishing", "Basketball", "Running", "Drone Racing", "Custom Drones", "Smoking (Foods)"
        };
        private static readonly string[] streetPre = new[]
        {
            "N", "S", "E", "W", ""
        };
        private static readonly string[] streetPost = new[]
        {
            "Rd", "Ct", "Ave", "Bch", "Blf", "Blvd", "Cir", "Dr", "Ext", "Fld", "Hwy", "Fwy", "Hollow", "Hills", "Haven", "Meadows", "Orchard", "Mtn", "Mountains", "Point", "Ridge", "Valley"
        };
        private static readonly string[] street = new[]
        {
            "1st", "2nd", "3rd", "4th", "5th", "Park", "Main", "Apache", "Ceder", "Aspen", "Oak", "Magnolia", "Dogwood", "Maple", "Lee", "Holly", "Pine", "Jackson", "Spruce", "Birch", "Willow", "Palo Verde", "Mesquite", "Sunset", "Navajo", "Quail", "Elm", "Redwood", "Cottonwood", "Canyon", "Canyon View", "Sunset"
        };
        private static readonly string[] city = new[]
        {
            "Washington", "Springfield", "Franklin", "Greenville", "Bristol", "Clinton", "Fairview", "Salem", "Madison", "Georgetown", "Arlington", "Centerville", "Lebanon", "Chester", "Dayton", "Dover", "Salem", "Oakland", "Milton", "Newport", "Riverside", "Ashland", "Bloomington", "Manchester", "Oxford", "Winchester", "Burlington", "Jackson", "Milford", "Clayton", "Auburn", "Kingston", "Lexington"
        };
        private static readonly string[] state = new[]
        {
            "AL", "AK", "AS", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "GU", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "MP", "OH", "OK", "OR", "PA", "PR", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "VI", "WA", "WV", "WI", "WY"
        };
        private static readonly string[] image = new[]
        {            
            "https://images.generated.photos/PJAklFnMfyYGZRGt8X79ZUVKeTVDlg6YQoR1aeEEW1Y/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzNzA0MDIuanBn.jpg",
            "https://images.generated.photos/Dgw4N3iTHZcj7Li9ZmH4LEIV-85sbg4bekJjq1RxuzM/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAyNzA3OTkuanBn.jpg",
            "https://images.generated.photos/zbbFQgBpGzWpc3YO5QACZw9mB60JexE9KZrgABA0ejs/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxMTU4NzIuanBn.jpg",
            "https://images.generated.photos/nHzlEWoDmPmWYlGm5O8X1_TnWSXl5T-sZ6-EWYxnzdw/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5MTQzMTcuanBn.jpg",
            "https://images.generated.photos/A7E1KReKKzTw0SfCuNkYkdubK5rSI8StOo5eRvyKER8/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2MTA3NzIuanBn.jpg",
            "https://images.generated.photos/oMPEckrfB6uSETCJTDyH-GHb-R1x68PQCD-OiHCFytI/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5NjIyNzMuanBn.jpg",
            "https://images.generated.photos/Z3Cq6PyHtPBaExJd6V1J29SSPuO4cQksIcw-h5tL3mI/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NDM3ODguanBn.jpg",
            "https://images.generated.photos/0Ru2FA4f1565NdtPC7DlY_G02xHSANvV8-45rkUpNtM/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA1Mzg4MTAuanBn.jpg",
            "https://images.generated.photos/qmQYNCYdtha12I4vN0IQJRucYyPRUluzZqlDG5lpuQE/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA1MzQ5MTQuanBn.jpg",
            "https://images.generated.photos/wW58vqRxElgDZyUDo0YB9ZGq4UqBB_4Q4WopTlEZonY/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4MzMzODAuanBn.jpg",
            "https://images.generated.photos/XicwMF-XE28RUIb59PPPEyix-M4lJHxyuwzeBdlSKZk/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5ODE0NzguanBn.jpg",
            "https://images.generated.photos/hyu7xe7QFWtfy9TT2NmB2CFqZR5Og-L0k5uRtTyoOzs/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzNDAzMzkuanBn.jpg",
            "https://images.generated.photos/EhSqlg93CfxBjkwrn5VDZj0MvbdM6pimlNrW6j3faeg/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0OTc5MDguanBn.jpg",
            "https://images.generated.photos/05dFo0kJ1gmUUwanQFuBcI9BxKNoXDRn3MeCo9qR0V0/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwMjY5MTIuanBn.jpg",
            "https://images.generated.photos/CjxdsUCA4FpFpy5h2akbN2b-nHCAvlqnPoQvMilmifA/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NjMwNTcuanBn.jpg",
            "https://images.generated.photos/RxNBAHWEiQfzt36KOqW0GMVrTwex_GYpxNo8yJ3XkCg/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxMzA5ODUuanBn.jpg",
            "https://images.generated.photos/ca0VmIeSRRu4a9sD4W__G7Gz4inMvpcAHeUJBxPDcio/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0ODM3NjEuanBn.jpg",
            "https://images.generated.photos/L25PjynAUpWh72nLpLeR5vaLipNcQvoaQdbPw4q_tos/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxNzI4NDcuanBn.jpg",
            "https://images.generated.photos/VPVdo9gV4Aip2XcVMzsP2HTEOENNxvLEEthNoTrmCHY/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4Mzc4MjQuanBn.jpg",
            "https://images.generated.photos/vKjJqwsWJmZYvzVnxpmvgJ8Eo0CafRiGc4L9aOwzc0g/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5NjU0NzEuanBn.jpg",
            "https://images.generated.photos/gDudJ4-neHrXd5vrmg1OFvoV3soCgAPR7CL0gUL9NSs/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxNDQ4NzYuanBn.jpg",
            "https://images.generated.photos/_CB7E6sRN7mVKJTwq2SDvbobmnZAuXG9sUPCZ3dFZRQ/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwMTc0NzQuanBn.jpg",
            "https://images.generated.photos/8gB9FPrQBtkVA0Z7OW2nVVc_Zyn1Julb735jUOr7liA/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0NTUxMTIuanBn.jpg",
            "https://images.generated.photos/TkAi6ZWr0bWYQS5wI30t5zHOLJdTJOnICa3uSYBOR_Q/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NzY3MDIuanBn.jpg",
            "https://images.generated.photos/SpXUxUdL2hKkFvhJ4S3GD7fs4uedJGu-Qewmrdi0wnw/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwMDc5NDQuanBn.jpg",
            "https://images.generated.photos/isOSgtwzeFCr_r0sEijlirFwkDIakE4-odBCH6uGh6M/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5Mjk2NTIuanBn.jpg",
            "https://images.generated.photos/hj32wNicF5NA8mvKC77Sp0mmVeA1L-Dr_CdVEiVTuEg/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5MTgzNTQuanBn.jpg",
            "https://images.generated.photos/NhyaovCZ8hpQHG0DrITpUM4HUw834bLY3fvNEs7Ub4k/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAyMTA2NTUuanBn.jpg",
            "https://images.generated.photos/g8eQddi3zVhV_-y1i1cfEXzqi6LvCqO33y3ALYDeb_Y/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzMjg5OTYuanBn.jpg",
            "https://images.generated.photos/F-0XaFSefjg-6zggFmmyojkHxiDJS7nNUHar6bqP81U/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwNDE4ODEuanBn.jpg",
            "https://images.generated.photos/GAZ2arcyMfIOQtYseRt4z4OYNQ73JQ4EPx9DVWmhpdk/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5NjE3ODUuanBn.jpg",
            "https://images.generated.photos/V2cYWOXUD6Zr8zZxKj0jHm57q0orQeJhMIU3SNp1ALA/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5MzM1OTIuanBn.jpg",
            "https://images.generated.photos/0Ru2FA4f1565NdtPC7DlY_G02xHSANvV8-45rkUpNtM/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA1Mzg4MTAuanBn.jpg",
            "https://images.generated.photos/9QSqEm10kstIuplBPCe3Wjvuwpiq21aYF8NnIEKEnFk/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2ODU1NTYuanBn.jpg",
            "https://images.generated.photos/RKvdkvAcsb8XG2Pnh3arOCG8cICrUU9-De9cqe5W_jQ/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NTE1NjMuanBn.jpg",
            "https://images.generated.photos/M4C1fp_cl70TYPEdlmAL9_rm58-L7X6A4cA7ZmmAenU/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA1OTczODYuanBn.jpg",
            "https://images.generated.photos/sMZA4_hKe_4cUDAm08NBIMP9arBuh1Rou79OVLc7yOA/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2MzY5MjYuanBn.jpg",
            "https://images.generated.photos/uRYpp26u3s-qU6Zfz3fRYT-c8KN6QpPp1lDJWOjSrAY/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzNTYyNjQuanBn.jpg",
            "https://images.generated.photos/AEm8a7aUdM3EJuAwXXrm9BFnAMFrB74vfiNk6cE0kG8/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwOTAwNDMuanBn.jpg",
            "https://images.generated.photos/UE-3ydttyPCU7Ig91OYWhqRKHojwC528KCwIw1qeXnQ/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2NjcyODMuanBn.jpg",
            "https://images.generated.photos/MOv6L4-zoPMnWH31tQ3IQUPpliu8c0nbwhCcWzK1SWU/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxOTAzMTUuanBn.jpg",
            "https://images.generated.photos/hy4brgDavCEjmg4NmB_15v6LFbZWT8pUS5Bi3CPr9Ys/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0ODY5NDEuanBn.jpg",
            "https://images.generated.photos/Z3Cq6PyHtPBaExJd6V1J29SSPuO4cQksIcw-h5tL3mI/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NDM3ODguanBn.jpg",
            "https://images.generated.photos/XukFlz9KnLrlsS5Qgd_t9P64xA8li_nmHdm0dKLTHCY/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0ODAxMjguanBn.jpg",
            "https://images.generated.photos/_K1la4T4jFisxLHl4n89i1laEikmcFR7N-AWvAwsiuE/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4Mzg3ODQuanBn.jpg",
            "https://images.generated.photos/7azzRuhC6dtsUOQkalDlns2N8DjZniu_NjkDe4o0H08/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4Nzg5MDMuanBn.jpg",
            "https://images.generated.photos/ofMmjw9WjXJfXxNZ56oQONkMziiVPLelX-0708e7elk/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwNjUzMzguanBn.jpg",
            "https://images.generated.photos/NJbCSYE_jlHzfKXv1NsR56QPIK_K5apyUYeIHLrrhg4/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAyNDg0OTQuanBn.jpg",
            "https://images.generated.photos/MJKWLJkjtNqfpMYu2MxpKlW2A_tttcf3v_BLNqYg28k/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2OTI2NTkuanBn.jpg",
            "https://images.generated.photos/_yh6wILgTUTgwvQlEYEoXQ1O9nHeEiBFDoFdyWYNxR0/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4MzA3MjYuanBn.jpg",
            "https://images.generated.photos/qiC4IciBnJL6gigrTFQKtGdSTEwArH_FZoOwyZm7gOA/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0Njc3NzguanBn.jpg",
            "https://images.generated.photos/BWyGM52AcVDumuTw6ELTpSkbQ6rFKOmwrenj8gQJ9mc/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NTM0MDAuanBn.jpg",
            "https://images.generated.photos/m8MEivwh1esyt2efHLV5-NQyTcqmuw1YgrkFodnCSOM/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwMDg1MjguanBn.jpg",
            "https://images.generated.photos/2FLYgUYSPdwJcp60E5xK4rjxihnbsahUDChCjtxHWEo/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0OTY0MTkuanBn.jpg",
            "https://images.generated.photos/qm_CXbq64GXdtaBNvdBdOJyNOgUHrxpyU4K1JRNdcvQ/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzNjgyMjEuanBn.jpg",
            "https://images.generated.photos/0W1J5b4AtQev_JBevy5hRezSvx0dCXe5PxR_FJBh8FU/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5NjkxNzkuanBn.jpg",
            "https://images.generated.photos/Axm0-ursqbnOd82c_Oo56xSPs1JuwGAuPHeUHZIwTlU/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAyODk2MTkuanBn.jpg",
            "https://images.generated.photos/nfOxqQYhOqBbXojanKoRamNAfq_etNCZ-iGqKSHNhEU/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2NzYwMzAuanBn.jpg",
            "https://images.generated.photos/JiToLDxKQVo-7jdP-NmqFSp6-l0S8l8-uVtykwLUIjk/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2NjA0OTIuanBn.jpg",
            "https://images.generated.photos/oJoj6S3mR55EDBti6rBXuKcPzRetpJVOdfNw9i_1yd4/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5Nzc2NjIuanBn.jpg",
            "https://images.generated.photos/at7X-eyGlX5shCQnZgs5JUajym7eb99Z5sB8i6I9jec/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAzMjA4NTEuanBn.jpg",
            "https://images.generated.photos/Tmd2YML7lG8nWK_FAlrttC2NvYlOH4XaD2ICgf0zC6k/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAyMTIxNzYuanBn.jpg",
            "https://images.generated.photos/CIv4IKpwEz5QsCoGiUReXaEdKpLA-HPwleSB4ljUAMg/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxNTM0NjEuanBn.jpg",
            "https://images.generated.photos/ev_CZrjMXMHNh2iNKrjC8A6oDFmJLxTsK69D_kYraKc/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA5MzU2OTIuanBn.jpg",
            "https://images.generated.photos/TepIwVUxaIcpw7YQjIH-L6groiqtOZkIoBBelMzeEeE/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxMzc3MjAuanBn.jpg",
            "https://images.generated.photos/1705mgv_IlPBEEyqtPQcG6mB1A4ZIT_IA4VFukR_Rnw/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA4NTA4OTMuanBn.jpg",
            "https://images.generated.photos/pjYWmKTtD5kQbLgHoKwAt1KkOgh57oDvTQNFiEzXQ7k/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA3NTE0NTQuanBn.jpg",
            "https://images.generated.photos/BHXxwQurOiU6zKAXVQkOu567Tdkox6sgJaaam9dcNdc/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA3MDQ5NTIuanBn.jpg",
            "https://images.generated.photos/fx9ai_oJnT-be02DNJ1swJlFduDA595cqLMTsHGYzZQ/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA2ODAyOTQuanBn.jpg",
            "https://images.generated.photos/zhrC0iYkizgy0qAS8t2G-gG38mJwMXcY-kkBA_Zs00I/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAxMzkzODEuanBn.jpg",
            "https://images.generated.photos/HKw-PqZcmKFPLRjmSilceNWerJFUbhZ8aXmGsFrPvuI/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzAwMjkwMTEuanBn.jpg",
            "https://images.generated.photos/Vfifv6BITncvrFjHxxa-jGbjj8YnLg-YtHSpiFalrBY/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA0NTI3NDkuanBn.jpg",
            "https://images.generated.photos/g436hyeDNlLDGm-tNhnqu429lJxzjxSSFCSzMN1osSo/rs:fit:128:128/Z3M6Ly9nZW5lcmF0/ZWQtcGhvdG9zL3Yz/XzA3MTc4MjguanBn.jpg"




        };

        private readonly ILogger<PeopleSearchController> _logger;

        public PeopleSearchController(ILogger<PeopleSearchController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PeopleSearch> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 25).Select(index => new PeopleSearch
            {
                Name = fName[rng.Next(fName.Length)] + " " + lName[rng.Next(lName.Length)],
                Address = rng.Next(1, 99999).ToString() + " " + streetPre[rng.Next(streetPre.Length)] + " " + street[rng.Next(street.Length)] + " " + streetPost[rng.Next(streetPost.Length)] + ", " + city[rng.Next(city.Length)] + ", " + state[rng.Next(state.Length)] + ", " + rng.Next(10000, 100000),
                Age = rng.Next(18, 99),
                Interests = interests[rng.Next(interests.Length)],
                Image = image[rng.Next(interests.Length)]
            })
            .ToArray();
        }
    }
}
