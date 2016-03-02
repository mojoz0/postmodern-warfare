
using System.IO; // Well, here we are! The other main file!
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Buffer=System.Text.StringBuilder;
using DateTime=System.DateTime;
using Type=System.Type;
using YamlDotNet.Serialization;
using UnityEngine;


public static class yml {

    public static Dictionary<string,Message> messages =
        new Dictionary<string,Message>();

    static Deserializer deserializer =
        new Deserializer();


    /** `yml` : **`constructor`**
     *
     * Instantiates a `Deserializer`, registers tags, &
     * reads data from the specified files. While the
     * usage of `static`s *and*  `constructor`s aren't
     * kosher in `Unity`, but in this case, it's ok, as
     * this has nothing to do with the `MonoBehaviour`
     * loading / instantiation process.
     **/
    static yml() {
        string  pre = "tag:yaml.org,2002:",
                ext = ".yml",
                dir =
#if UNITY_EDITOR
                    Directory.GetCurrentDirectory()
                        +"/Assets/Resources/";
#else
                    Application.dataPath+"/Resources/";
#endif

        // mapping of all the tags to their types
        var tags = new Dictionary<string,Type> {
            { "regex", typeof(Regex) },
            { "date", typeof(DateTime) },
            { "message", typeof(Message) }};

        foreach (var tag in tags)
            deserializer.RegisterTagMapping(
                pre+tag.Key, tag.Value);

        var files = new[] { "messages" };

        foreach (var file in files)
            foreach (var kvp in deserializer.Deserialize<Dictionary<string,Message>>(
                                GetReader(Path.Combine(dir,file)+ext)))
                messages[kvp.Key] = kvp.Value;
    }


    /** `GetReader()` : **`StringReader`**
     *
     * Gets the `*.yml` file in the main directory only
     * if it exists and has the proper extension.
     *
     * - `throw` : **`Exception`**
     *     if the file does not exist
     **/
    static StringReader GetReader(string file) {
        if (!File.Exists(file))
            throw new System.Exception("404");
                //$"YAML 404: {file}");
        var buffer = new Buffer();
        foreach (var line in File.ReadAllLines(file))
            buffer.AppendLine(line);
        return new StringReader(buffer.ToString());
    }




    /** `Deserialize()` : **`function`**
     *
     * Called without type arguments, this will simply
     * deserialize into the `data` object. This is used
     * only by the `static` constructor to get data out
     * of the rest of the files (skipping the few files
     * which are specified above).
     *
     * - `file` : **`string`**
     *     filename to look for
     *
     * - `throw` : **`IOException`**
     **/
    //static void Deserialize(string file) {
    //    foreach (var kvp in deserializer.Deserialize<Dictionary<string,object>>(GetReader(file))) messages[kvp.Key] = kvp.Value; }


    /** `Deserialize<T>()` : **`<T>`**
     *
     * Returns an object of type `<T>` from the
     * dictionary if it exists.
     *
     * - `<T>` : **`Type`**
     *      type to look for, and then to cast to, when
     *      deserializing the data from the file.
     *
     * - `s` : **`string`**
     *     key to look for
     *
     * - `throw` : **`Exception`**
     *     There is no key at `data[s]`, or some other
     *     problem occurs when attempting to cast the
     *     object to `<T>`.
     **/
    public static Message DeserializeMessage(string s) {
        Message o;
        if (!messages.TryGetValue(s,out o))
            throw new System.Exception("badcast");
                //$"Bad cast: {typeof(T)} as {s}");
        return o;
    }





    /** `md()` : **`string`**
     *
     * Adds support for `Markdown`, and can be called on
     * any `string`. Formats the `Markdown` syntax into
     * `HTML`. Currently removes all `<p>` tags.
     *
     * - `s` : **`string`**
     *    `string` to be formatted.
     **/
    public static string md(this string s) {
        return new Buffer(Markdown.Transform(s))
            .Replace("<em>","<i>")
            .Replace("</em>","</i>")
            .Replace("<blockquote>","<i>")
            .Replace("</blockquote>","</i>")
            .Replace("<strong>","<b>")
            .Replace("</strong>","</b>")
            .Replace("<h1>","<size=48><color=#98C8FC>")
            .Replace("</h1>","</color></size>")
            .Replace("<h2>","<size={36><color=#EEEEE>")
            .Replace("</h2>","</color></size>")
            .Replace("<h3>","<size=24><color=#DDDDDD>")
            .Replace("</h3>","</color></size>")
            .Replace("<pre>").Replace("</pre>")
            .Replace("<code>").Replace("</code>")
            .Replace("<ul>").Replace("</ul>")
            .Replace("<li>").Replace("</li>")
            .Replace("<p>").Replace("</p>")
            /* custom tags */
            .Replace("<help>","<color=#9CDF91>")
            .Replace("</help>","</color>")
            .Replace("<cmd>", "<color=#BBBBBB>")
            .Replace("</cmd>","</color>")
            .Replace("<warn>","<color=#FA2363>")
            .Replace("</warn>","</color>")
            .Replace("<cost>","<color=#FFDBBB>")
            .Replace("</cost>","</color>")
            .Replace("<red>","<color=#f26a6a>")
            .Replace("</red>","</color>")
            .Replace("<orange>", "<color=#f6a72e>")
            .Replace("</orange>", "</color>")
            .Replace("<yellow>", "<color=#fff16b>")
            .Replace("</yellow>", "</color>")
            .Replace("<green>", "<color=#73d279>")
            .Replace("</green>", "</color>")
            .Replace("<blue>", "<color=#73aed2>")
            .Replace("</blue>", "</color>")
            .Replace("<indigo>", "<color=#b695ea")
            .Replace("</indigo>", "</color>")
            .Replace("<violet>", "<color=#d495ea>")
            .Replace("</violet>", "</color>")
            .ToString();
    }


    /** `Replace()` : **`string`**
     *
     * Adds an overload to the existing `Replace()` that
     * takes a single argument, for removing things instead
     * of replacing them.
     *
     * - `s` : **`string`**
     *    `string` to be formatted.
     *
     * - `newValue` : **`string`**
     *    replacement `string` to insert.
     **/
    public static string Replace(
                    this string s,
                    string newValue) {
        return s.Replace(newValue,""); }


    public static Buffer Replace(
                    this Buffer sb,
                    string s) {
        return sb.Replace(s,""); }



}









