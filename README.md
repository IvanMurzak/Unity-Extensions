# 1. Add NPM repository to project
Modify manifest.json file. The file located under directory "Packages".
Add "scopedRegistries" to the end of the file.
<pre><code>{
  "dependencies": {
    
  },
  "scopedRegistries": [
    {
      "name": "Unity Extensions",
      "url": "https://registry.npmjs.org",
      "scopes": [
        "extensions.unity"
      ]
    }
  ]
}
</code></pre>

# 2. Add extensions
When NPM repository added, you may optionally add all extensions from the list below. You just need to add relevant line to the "dependencies" section. If you are adding multiple lines don't forget to add ',' in the end of each line.

## Unity-Extensions
<pre><code>{
  "dependencies": {
    "extensions.unity.base": "1.5.0"
  }
}
</code></pre>

## Unity-UI-Extensions
<pre><code>{
  "dependencies": {
    "extensions.unity.ui": "1.2.2"
  }
}
</code></pre>

## Unity Network REST extension
<pre><code>{
  "dependencies": {
    "extensions.unity.network": "1.0.1"
  }
}
</code></pre>

## Unity UniRx extensions
<pre><code>{
  "dependencies": {
    "extensions.unity.unirx": "1.1.3"
  }
}
</code></pre>

## Example - All Extensions included
<pre><code>{
  "dependencies": {
    "extensions.unity.base": "1.5.0",
    "extensions.unity.ui": "1.2.2",
    "extensions.unity.network": "1.0.1",
    "extensions.unity.unirx": "1.1.3"
  }
}
</code></pre>