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
![npm](https://img.shields.io/npm/v/extensions.unity.base)
<pre><code>{
  "dependencies": {
    "extensions.unity.base": "1.9.0"
  }
}
</code></pre>

## Unity-Saver
![npm](https://img.shields.io/npm/v/extensions.unity.saver)
<pre><code>{
  "dependencies": {
    "extensions.unity.saver": "1.0.1"
  }
}
</code></pre>

## Unity Network REST extension
![npm](https://img.shields.io/npm/v/extensions.unity.network)
<pre><code>{
  "dependencies": {
    "extensions.unity.network": "1.3.3"
  }
}
</code></pre>

## Unity IAP extension
![npm](https://img.shields.io/npm/v/extensions.unity.iap.store)
<pre><code>{
  "dependencies": {
    "extensions.unity.iap.store": "2.0.6"
  }
}
</code></pre>

## Unity UniRx extensions
![npm](https://img.shields.io/npm/v/extensions.unity.unirx)
<pre><code>{
  "dependencies": {
    "extensions.unity.unirx": "1.1.5"
  }
}
</code></pre>

## Unity-UI-Extensions
![npm](https://img.shields.io/npm/v/extensions.unity.ui)
<pre><code>{
  "dependencies": {
    "extensions.unity.ui": "1.3.2"
  }
}
</code></pre>

## Shapes RectTransform
![npm](https://img.shields.io/npm/v/extensions.unity.shapes.recttransform)

![Shapes RectTransform demo](https://media.giphy.com/media/nn779lmlBy5FgFwQqB/giphy.gif)
<pre><code>{
  "dependencies": {
    "extensions.unity.shapes.recttransform": "1.0.2"
  }
}
</code></pre>

## Example - All Extensions included
<pre><code>{
  "dependencies": {
    "extensions.unity.base": "1.9.0",
    "extensions.unity.network": "1.3.3",
    "extensions.unity.iap.store": "2.0.6",
    "extensions.unity.ui": "1.3.2",
    "extensions.unity.unirx": "1.1.5",	
    "extensions.unity.shapes.recttransform": "1.0.2"
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
