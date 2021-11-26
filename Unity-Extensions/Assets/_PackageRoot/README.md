# Unity-Extensions

## Variant 1
- Select "Add package from git URL"
- Paste <code>https://github.com/IvanMurzak/Unity-Extensions.git#upm</code>

![alt text](https://neogeek.dev/images/creating-custom-packages-for-unity-2018.3--package-manager.png)

## Variant 2
Modify manifest.json file.
<pre><code>{
    "dependencies": {
        "extensions.unity.base": "https://github.com/IvanMurzak/Unity-Extensions.git#upm"
    }
}
</code></pre>
