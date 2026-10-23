------------------
---- MONITORS ----
------------------

-- See https://wiki.hypr.land/Configuring/Basics/Monitors/

hl.monitor({
    output   = "HDMI-A-1",
    mode     = "1920x1080@60.00",
    position = "auto",
    scale    = "1",
})


hl.workspace_rule({ workspace = "1", monitor = "Virtual-1", persistent = true })
hl.workspace_rule({ workspace = "2", monitor = "Virtual-1", persistent = true })
hl.workspace_rule({ workspace = "3", monitor = "Virtual-1", persistent = true })
hl.workspace_rule({ workspace = "4", monitor = "Virtual-1", persistent = true })
hl.workspace_rule({ workspace = "5", monitor = "Virtual-1", persistent = true })

hl.window_rule({
    match = { class = "brave" },
    opacity = "1.0 override", })
