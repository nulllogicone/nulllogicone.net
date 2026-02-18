# SPARQL Ontop UI Tweaks

Fun CSS customizations for your Ontop SPARQL endpoint to give it some nulllogicone.net style! 🔮

## What It Does

The `sparql-ontop.css` file applies:
- **Gradient purple background** matching the nulllogicone.net brand
- **Modern, card-style interface** with rounded corners and shadows
- **Styled query editor** with purple accent borders
- **Beautiful result tables** with hover effects
- **Custom scrollbars** with gradient styling
- **Nulllogicone.net branding** header injection
- **Enhanced buttons and alerts** with gradients

## How to Use

### Option 1: Browser Extension (Stylus/Stylish)

1. Install [Stylus](https://github.com/openstyles/stylus) (recommended) or Stylish browser extension
2. Create a new style for your Ontop domain (e.g., `http://10.0.3.4:8080/*`)
3. Copy the contents of `sparql-ontop.css` and paste into the style editor
4. Save and enjoy!

### Option 2: Inject Into Ontop Deployment

If you control the Ontop deployment, you can inject this CSS into the web interface:

1. Locate your Ontop's `webapp` directory
2. Add `sparql-ontop.css` to the assets folder
3. Inject it into the main `index.html` or template:
   ```html
   <link rel="stylesheet" href="/assets/sparql-ontop.css">
   ```

### Option 3: Docker Volume Mount

If running Ontop in Docker, mount it as a volume:

```yaml
services:
  ontop:
    image: ontop/ontop
    volumes:
      - ./css/sparql-ontop.css:/opt/ontop/webapp/assets/custom.css
```

Then modify the entry point to inject the CSS link.

## Preview

- **Before**: Standard Ontop/YASGUI interface
- **After**: Purple gradient beauty with nulllogicone.net branding! ✨

## Notes

- All styles use `!important` to override Ontop's defaults
- Works with YASGUI-based interfaces
- Designed for desktop/laptop viewing (not optimized for mobile)
- Safe to use - only affects visual appearance, not functionality

## Credits

Part of the nulllogicone.net semantic API project 🎨
