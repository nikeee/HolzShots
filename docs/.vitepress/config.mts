import { defineConfig } from "vitepress"

// https://vitepress.dev/reference/site-config
export default defineConfig({
  title: "HolzShots",
  description: "A lightweight screenshot utility that gets out of your way.",
  outDir: "dist",
  head: [
    // TODO: Most of them are not needed for iOS icons, clean up later
    [
      "link",
      { rel: "apple-touch-icon-precomposed", sizes: "120x120", href: "assets/img/120x120.png" },
    ],
    [
      "link",
      { rel: "apple-touch-icon-precomposed", sizes: "512x512", href: "assets/img/512x512.png" },
    ],
    [
      // TODO: Don't include this in development mode
      "script",
      {},
      `
      var _paq = _paq || [];
      _paq.push(["disableCookies"], ["trackPageView"], ["enableLinkTracking"]);
      (function() {
        var u="https://stats.holzshots.net/";
        _paq.push(["setTrackerUrl", u+"piwik.php"], ["setSiteId", "2"]);
        var d=document, g=d.createElement("script"), s=d.getElementsByTagName("script")[0];
        g.type="text/javascript"; g.async=true; g.defer=true; g.src=u+"piwik.js"; s.parentNode.insertBefore(g,s);
      })();
      `,
    ],
  ],
  themeConfig: {
    // https://vitepress.dev/reference/default-theme-config
    nav: [
      { text: "Home", link: "/" },
      { text: "Getting Started", link: "/getting-started" },
      { text: "Source", link: "https://github.com/nikeee/HolzShots" },
    ],

    sidebar: [
      {
        text: "Examples",
        items: [
          { text: "Issues", link: "https://github.com/nikeee/HolzShots/issues" },
          // { text: "Runtime API Examples", link: "/api-examples" }
        ]
      }
    ],

    socialLinks: [
      { icon: "github", link: "https://github.com/nikeee/HolzShots" }
    ]
  }
})
