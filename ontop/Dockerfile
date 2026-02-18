FROM ontop/ontop:5.1.0

# Copy configuration files
# Note: ontop-azure.properties is a template that uses environment variables
# The entrypoint.sh script will substitute these at runtime
# No passwords are baked into this image!
COPY nulllogicone.obda.ttl /opt/ontop/input/
COPY nulllogicone.ttl /opt/ontop/input/
COPY ontop-azure.properties /opt/ontop/input/ontop.properties.template

# Copy SQL Server JDBC driver
COPY jdbc/mssql-jdbc-12.8.1.jre11.jar /opt/ontop/jdbc/

# Copy and setup entrypoint script for environment variable substitution
COPY entrypoint.sh /opt/ontop/

# Expose SPARQL endpoint port
EXPOSE 8080

# Run Ontop endpoint via entrypoint script that substitutes environment variables
ENTRYPOINT ["/bin/bash", "/opt/ontop/entrypoint.sh"]
CMD ["endpoint", "-m", "/opt/ontop/input/nulllogicone.obda.ttl", "-t", "/opt/ontop/input/nulllogicone.ttl", "-p", "/opt/ontop/input/ontop.properties"]
