﻿namespace Sitecore.Feature.Blog.Tokens
{
    using System;
    using System.Linq.Expressions;
    using ContentSearch.SearchTypes;
    using Data.Items;
    using Domain;

    public interface IToken
    {
        string Regex { get; }

        string Token { get; }

        string Friendly { get; }

        Expression<Func<SearchResultItem, string, bool>> Filter  { get; }

        Action<BlogContext, string> MapToContext { get; }

        Func<Item, object> MapToPermalink { get; }

        Action<BlogContext, object> AddToPropertyBag { get; }
    }
}
