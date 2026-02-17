FROM ontop/ontop:5.1.0

# Copy configuration files
# Note: ontop-azure.properties uses environment variables for credentials
# No passwords are baked into this image!
COPY nulllogicone.obda.ttl /opt/ontop/input/
COPY nulllogicone.ttl /opt/ontop/input/
COPY ontop-azure.properties /opt/ontop/input/ontop.properties

# Copy SQL Server JDBC driver
COPY jdbc/mssql-jdbc-12.8.1.jre11.jar /opt/ontop/jdbc/

# Expose SPARQL endpoint port
EXPOSE 8080

# Run Ontop endpoint
ENTRYPOINT ["/opt/ontop/ontop"]
CMD ["endpoint", "-m", "/opt/ontop/input/nulllogicone.obda.ttl", "-t", "/opt/ontop/input/nulllogicone.ttl", "-p", "/opt/ontop/input/ontop.properties"]
