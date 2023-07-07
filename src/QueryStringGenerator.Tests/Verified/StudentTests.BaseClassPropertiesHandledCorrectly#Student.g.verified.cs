﻿//HintName: Student.g.cs
// <auto-generated />

namespace QueryStringGenerator.Tests.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("QueryStringGenerator", "1.0.0")]
    public static class QueryStringExtensionForStudent
    {
        public static string ToQueryStringFromStudent(this Student _this)
        {
            if (_this == null)
            {
                return string.Empty;
            }

            var sb = new System.Text.StringBuilder();

            if (_this.School != null)
            {
                switch (_this.School)
                {
                    case QueryStringGenerator.Tests.Models.School.JediAcademy:
                        sb.Append("&school=jediAcademy");
                        break;

                    case QueryStringGenerator.Tests.Models.School.StarfleetAcademy:
                        sb.Append("&school=starfleetAcademy");
                        break;

                    case QueryStringGenerator.Tests.Models.School.XaviersSchoolForGiftedYoungsters:
                        sb.Append("&school=xaviersSchoolForGiftedYoungsters");
                        break;

                    default:
                        throw new System.NotImplementedException();
                }
            }

            return sb.ToString();
        }
    }
}