#r "..\..\..\Domain\bin\Debug\netcoreapp2.1\Domain.dll"
#r "..\..\..\Common\bin\Debug\netcoreapp2.1\Common.dll"

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

Output.BuildAction = BuildAction.None;
Output.SetExtension(".cs");
Output.WriteLine($@"//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
//<auto-generated>
//  This code was generated by a tool
//  Runtime Version: { Environment.Version.ToString() }
//
//  Changes to this file may cause incorrect behavior and will be lost if
//  the code is regenerated.
//</auto-generated>
//- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
using System;
using System.CodeDom.Compiler;

namespace Api.Test.Framework.Builders
{{
{ BuildTypes() }
}}");

private string BuildTypes()
{
    var output = new StringBuilder();

    try
    {
        var modelType = typeof(Domain.Category);    
        var modelAssembly = modelType.Assembly;
        IEnumerable<Type> modelAssemblyTypes;

        try
        {
            modelAssemblyTypes = modelAssembly.DefinedTypes;
        }
        catch (ReflectionTypeLoadException ex)
        {
            modelAssemblyTypes = ex.Types.Where(x => x != null).ToList();
        }

        var modelTypeParent = typeof(Common.Utils.Entity);
        var modelTypes = modelAssemblyTypes
            .Where(t => !t.IsGenericTypeDefinition && !t.IsAbstract && modelTypeParent.IsAssignableFrom(t))
            .OrderBy(t => t.Name)
            .ToList();

        foreach (var type in modelTypes)
        {
            var typeName = type.Name;
            var typeFullName = type.FullName;
            var builderName = $"{typeName}Builder";
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(x => x.CanWrite)
                .OrderBy(x => x.Name)
                .ToList();

            output.AppendLine($@"
    [GeneratedCode(""Builders"", ""1.0"")]
    public partial class {builderName} : Builder<{typeFullName}>
    {{
{ BuildProperties(properties) }
{ BuildBuildMethod(typeFullName, properties) }
{ BuildDefaultMethod(builderName) }
{ BuildWithMethods(builderName, properties) }
    }}");
        }
    }
    catch (Exception ex)
    {
        return $"// {ex.ToString()}";
    }

    return output.ToString();
}

private string BuildProperties(IEnumerable<PropertyInfo> properties)
{
    var output = new StringBuilder();

    foreach(var property in properties)
    {
        var propertyName = property.Name;
        var propertyTypeName = "object";

        try
        {
            propertyTypeName = property.PropertyType.ToString().Replace("`1[", "<").Replace(']', '>');
            output.AppendLine($@"        private Lazy<{propertyTypeName}> _{CamelCase(propertyName)} = new Lazy<{propertyTypeName}>(default({propertyTypeName}));");
        }
        catch (Exception ex)
        {
            output.AppendLine($"\r\n// An error occurred while examining property { propertyName }: { ex.Message }\r\n");
        }
    }

    return output.ToString();
}

private string BuildBuildMethod(string typeFullName, List<PropertyInfo> properties)
{
    var output = new StringBuilder();
    output.AppendLine($@"        [GeneratedCode(""ModelBuilders"", ""1.0"")]
        public override {typeFullName} Build()
        {{
            if (Object?.IsValueCreated != true)
            {{
                Object = new Lazy<{typeFullName}>(new {typeFullName}
                {{");

    foreach (var property in properties)
    {
        var propertyName = property.Name;
        string propertyTypeName = "object";

        try
        {
            propertyTypeName = property.PropertyType.ToString().Replace("`1[", "<").Replace(']', '>');
            output.AppendLine($@"                    {propertyName} = _{CamelCase(propertyName)}.Value,");
        }
        catch (Exception ex)
        {
            output.AppendLine($@"                    // An error occurred while examining property {propertyName}: {ex.Message}\r\n");
        }
    }

    output.AppendLine(@"                });
            }

            PostBuild(Object.Value);

            return Object.Value;
        }");

    return output.ToString();
}

private string BuildDefaultMethod(string builderName)
{
    var output = new StringBuilder();

    output.AppendLine($@"        [GeneratedCode(""ModelBuilders"", ""1.0"")]
        public static { builderName } Default()
        {{
            return new { builderName }();
        }}");

    return output.ToString();
}

private string BuildWithMethods(string builderName, IEnumerable<PropertyInfo> properties)
{
    var output = new StringBuilder();

    foreach(var property in properties)
    {
        var propertyName = property.Name;
        var fieldName = $"_{ CamelCase(propertyName) }";
        var propertyTypeName = "object";

        try
        {
            propertyTypeName = property.PropertyType.ToString().Replace("`1[", "<").Replace(']', '>');

            output.AppendLine($@"        [GeneratedCode(""ModelBuilders"", ""1.0"")]
        public { builderName } With{ propertyName }({ propertyTypeName } value)
        {{
            return With{ propertyName }(() => value);
        }}

        [GeneratedCode(""ModelBuilders"", ""1.0"")]
        public { builderName } With{ propertyName }(Func<{ propertyTypeName }> func)
        {{
            { fieldName } = new Lazy<{ propertyTypeName }>(func);
            return this;
        }}

        [GeneratedCode(""ModelBuilders"", ""1.0"")]
        public { builderName } Without{ propertyName }()
        {{
            { fieldName } = new Lazy<{ propertyTypeName }>(default({ propertyTypeName }));
            return this;
        }}");

        }
        catch (Exception ex)
        {
            output.AppendLine($"\r\n// An error occurred while examining property { propertyName }: { ex.Message }");
        }
    }

    return output.ToString();
}

private string CamelCase(string value)
{
    return $"{value.Substring(0, 1).ToLowerInvariant()}{value.Substring(1)}";
}
