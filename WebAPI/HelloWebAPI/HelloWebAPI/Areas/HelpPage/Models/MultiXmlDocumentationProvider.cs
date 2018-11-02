﻿using HelloWebAPI.Areas.HelpPage.ModelDescriptions;
using System;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;

namespace HelloWebAPI.Areas.HelpPage.Models
{
    public class MultiXmlDocumentationProvider : IDocumentationProvider, IModelDocumentationProvider
    {
        private readonly XmlDocumentationProvider[] Providers;
        public MultiXmlDocumentationProvider(params string[] paths)
        {
            this.Providers = paths.Select(p => new XmlDocumentationProvider(p)).ToArray();
        }

        public string GetDocumentation(MemberInfo subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        public string GetDocumentation(Type subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        public string GetDocumentation(HttpControllerDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        public string GetDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        public string GetDocumentation(HttpParameterDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        public string GetResponseDocumentation(HttpActionDescriptor subject)
        {
            return this.GetFirstMatch(p => p.GetDocumentation(subject));
        }

        private string GetFirstMatch(Func<XmlDocumentationProvider, string> expr)
        {
            return this.Providers
                .Select(expr)
                .FirstOrDefault(p => !String.IsNullOrWhiteSpace(p));
        }
    }
}