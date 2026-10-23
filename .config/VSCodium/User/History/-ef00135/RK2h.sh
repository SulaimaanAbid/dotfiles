A_1080=400
B_1080=400

resolution=$(hyprctl -j monitors | jq -r '.[] | select(.focused==true) | .height / .scale' | awk -F'.' '{print $1}')
hypr_scale=$(hyprctl -j monitors | jq -r '.[] | select(.focused==true) | .scale')

echo "Resolution: $resolution"
echo "Scale: $hypr_scale"

echo "Top margin:"
awk "BEGIN {printf \"%.0f\n\", $A_1080 * 1080 * $hypr_scale / $resolution}"

echo "Bottom margin:"
awk "BEGIN {printf \"%.0f\n\", $B_1080 * 1080 * $hypr_scale / $resolution}"