--------------------------------
---- WINDOWS AND WORKSPACES ----
--------------------------------

-- See https://wiki.hypr.land/Configuring/Basics/Window-Rules/
-- and https://wiki.hypr.land/Configuring/Basics/Workspace-Rules/

-- Example window rules that are useful

local suppressMaximizeRule = hl.window_rule({
    -- Ignore maximize requests from all apps. You'll probably like this.
    name  = "suppress-maximize-events",
    match = { class = ".*" },

    suppress_event = "maximize",
})
-- suppressMaximizeRule:set_enabled(false)

hl.window_rule({
    -- Fix some dragging issues with XWayland
    name  = "fix-xwayland-drags",
    match = {
        class      = "^$",
        title      = "^$",
        xwayland   = true,
        float      = true,
        fullscreen = false,
        pin        = false,
    },

    no_focus = true,
})


hl.window_rule({
    match = { class = "brave-browser" },
    opacity = "1.0 override 1.0 override 1.0 override", })

hl.window_rule({
    match = { class = "kitty" },
    opacity = "0.7 override 0.5 override 0.8 override"
})

hl.layer_rule({
    match = {namespace = "rofi"},
    blur = true,
    animation = "slide bottom",
    ignore_alpha = 0.5,
    -- dim_around = true,
})

hl.layer_rule ({
    match = {namespace="swaync-control-center"},
    animation = "slide right",
    blur = true,
    ignore_alpha = 0.5,
})
-- Layer rules also return a handle.
-- local overlayLayerRule = hl.layer_rule({
--     name  = "no-anim-overlay",
--     match = { namespace = "^my-overlay$" },
--     no_anim = true,
-- })
-- overlayLayerRule:set_enabled(false)

-- Hyprland-run windowrule
-- hl.window_rule({
--     name  = "move-hyprland-run",
--     match = { class = "hyprland-run" },

--     move  = "20 monitor_h-120",
--     float = true,
-- })
