using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagegram.Functions.Extensions
{
    public static class FormCollectionExtensions
    {
        /// <summary>
        /// Attempts to bind a form collection to a model of type <typeparamref name="T" />.
        /// </summary>
        /// <typeparam name="T">The model type. Must have a public parameterless constructor.</typeparam>
        /// <param name="form">The form data to bind.</param>
        /// <returns>A new instance of type <typeparamref name="T" /> containing the form data.</returns>
        public static T BindToModel<T>(this IFormCollection form) where T : new()
        {
            var props = typeof(T).GetProperties();
            var instance = Activator.CreateInstance<T>();
            var formKeyMap = form.Keys.ToDictionary(k => k.ToUpper(), k => k);

            foreach (var p in props)
            {
                if (p.CanWrite && formKeyMap.ContainsKey(p.Name.ToUpper()))
                {
                    if (p.PropertyType == typeof(int))
                    {
                        p.SetValue(instance, int.Parse(form[formKeyMap[p.Name.ToUpper()]].FirstOrDefault()));
                    }
                    else if(p.PropertyType == typeof(string))
                    {
                        p.SetValue(instance, form[formKeyMap[p.Name.ToUpper()]].FirstOrDefault());
                    }
                }
            }

            return instance;
        }
    }
}
