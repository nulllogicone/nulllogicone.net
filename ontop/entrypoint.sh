#!/bin/bash
set -e

# Substitute environment variables in properties file using sed
# Write to /tmp since /opt/ontop/input/ is read-only
sed -e "s|\${SQL_SERVER}|${SQL_SERVER}|g" \
    -e "s|\${SQL_USER}|${SQL_USER}|g" \
    -e "s|\${SQL_PASSWORD}|${SQL_PASSWORD}|g" \
    /opt/ontop/input/ontop.properties.template > /tmp/ontop.properties

# Start Ontop with the processed properties file from /tmp
exec /opt/ontop/ontop endpoint -m /opt/ontop/input/nulllogicone.obda.ttl -t /opt/ontop/input/nulllogicone.ttl -p /tmp/ontop.properties
