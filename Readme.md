## Sitecore.Feature.Blog

The Sitecore.Feature.Blog library is an accelerator that provides blogging functionality, based on the helix design principles.

### Getting Started

A Sitecore installation package is provided for installing the blog module into your Sitecore environment.

> TBD Installation Process

To extend the blog module, use NuGet to install the Sitecore.Feature.Blog package as a DevDependency:

> TDB Package


After installation of the package within an environment, to create a blogging area, simply create an item that is derived from the 'Blog' template. All categories, authors, and tags will be managed in a simple Sitecore Item Bucket. 

### Resolving URLs

The blogging module uses custom routing through the use of tokens and resolvers that give the ability to generate any type of URL based on information stored within the bucket.

#### Tokens

Tokens provide the ability to extract data from a URL that then can be used to query or map the URL to an item within Sitecore. All tokens must implement the _IToken_ Interface and be configured at _/sitecore/blog/tokens_

Out of the box, the blogging module supports the following tokens:

* **$slug** is mapped to the slug field of an item
  *  A slug is used to identity a unique item within Sitecore
* **$tag** is mapped to the Tag Name of a Tag
  * Derived from the _SlugToken_
* **$category** is mapped to the Category Name of a Category
  * Derived from the _SlugToken_
* **$year** is mapped to the Post Year of a Post
* **$$month** is mapped to the Post Month of a Post
* **$author** is mapped to the Author Full Name
  * Derived from the _SlugToken_
* **$page** is mapped to an integer to provide pagination support
```xml
<?xml version="1.0"?>
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <blog>
      <tokens>
        <token type="Sitecore.Feature.Blog.Tokens.SlugToken, Sitecore.Feature.Blog">
          <friendly>slug</friendly>
          <token>$slug</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.TagName, Sitecore.Feature.Blog">
          <friendly>tag</friendly>
          <token>$tag</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.CategoryName, Sitecore.Feature.Blog">
          <friendly>category</friendly>
          <token>$category</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.PostYearToken, Sitecore.Feature.Blog">
          <friendly>year</friendly>
          <token>$year</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.PostMonthToken, Sitecore.Feature.Blog">
          <friendly>month</friendly>
          <token>$month</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.AuthorToken, Sitecore.Feature.Blog">
          <friendly>author</friendly>
          <token>$author</token>
        </token>
        <token type="Sitecore.Feature.Blog.Tokens.PageToken, Sitecore.Feature.Blog">
          <friendly>page</friendly>
          <token>$page</token>
        </token>
      </tokens>
    <blog>
  <sitecore>
</configuration>
```


#### Resolvers

A resolver resolves the requested url to a configured permalink. A resolver can contain multiple permalinks that it can resolve. All resolvers must implement, _IResolver_ and must be configured at _/sitecore/blog/resolvers_

Resolvers responsibilities are:

* Map a permalink to the requested url
* Tokenize the request
* Generate an Item Url
* Resolve the Item that matches the permalink

A resolver can contain multiple permalinks that can contain any number of tokens. If a token has been specified within a permalink and it is not found as a configured token, the token will be skipped. Additionally, a Resolver can specify a configured template id that will restrict the search to only return results of that template.

Out of the box, the blogging module comes with two resolvers:

* DefaultResolver - can be used for most cases where the URL can be mapped directly to an Item within Sitecore
* ArchiveResolver - special case resolver that maps the Item Resolved to the parent blog

```xml
<?xml version="1.0"?>
<configuration xmlns:patch="http://www.sitecore.net/xmlconfig/">
  <sitecore>
    <blog>
      <resolvers>
        <resolver desc="post" type="Sitecore.Feature.Blog.Resolvers.DefaultResolver, Sitecore.Feature.Blog" resolve="true">
          <template>{bd48d35d-ae82-4269-8e9e-b21a5f17b363}</template>
          <permalinks hint="list">
            <permalink>/$year/$month/$slug</permalink>
            <permalink>/post/$slug</permalink>
          </permalinks>
        </resolver>
        <resolver desc="tag" type="Sitecore.Feature.Blog.Resolvers.DefaultResolver, Sitecore.Feature.Blog" resolve="true">
          <template>{b05c5f99-615b-46e4-a0f0-fb43e62c404f}</template>
          <permalinks hint="list">
            <permalink>/tag/$tag</permalink>
            <permalink>/tag/$tag/page/$page</permalink>
          </permalinks>
        </resolver>
        <resolver desc="category" type="Sitecore.Feature.Blog.Resolvers.DefaultResolver, Sitecore.Feature.Blog" resolve="true">
          <template>{b8b0484c-f1a9-4a18-afa4-a7b942063d54}</template>
          <permalinks hint="list">
            <permalink>/category/$category</permalink>
            <permalink>/category/$category/page/$page</permalink>
          </permalinks>
        </resolver>
        <resolver desc="archive" type="Sitecore.Feature.Blog.Resolvers.ArchiveResolver, Sitecore.Feature.Blog" resolve="true">
          <template></template>
          <permalinks hint="list">
            <permalink>/page/$page</permalink>
            <permalink>/$year/$month</permalink>
            <permalink>/archive/$year/$month</permalink>
            <permalink>/archive/$year/$month/page/$page</permalink>
          </permalinks>
        </resolver>
        <resolver desc="author" type="Sitecore.Feature.Blog.Resolvers.DefaultResolver, Sitecore.Feature.Blog" resolve="true">
          <template>{702b64d5-58da-475f-843f-5bdf09a40e36}</template>
          <permalinks hint="list">
            <permalink>/author/$author</permalink>
          </permalinks>
        </resolver>
      </resolvers>   
    <blog>
  <sitecore>
</configuration>